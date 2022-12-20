using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using XtermBlazor;

namespace BlazorApp.Service.impl;

public class XtermStream : Stream
{
    private bool  _canRead, _canWrite;
    private Xterm _xterm;


    public XtermStream(Xterm xterm)
    {
        _canRead  = true;
        _canWrite = true;
        _xterm    = xterm;
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        // Console.WriteLine($"str={System.Text.Encoding.UTF8.GetString(buffer)}，offset={offset}   count={count}");
        _xterm.WriteLine(buffer);
        // _xterm.WriteLine(Encoding.UTF8.GetString(buffer));
        // Write(new ReadOnlySpan<byte>(buffer, offset, count));
    }

    public override void WriteByte(byte value) => Write(new ReadOnlySpan<byte>(in value));

    public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled(cancellationToken);
        }

        try
        {
            Write(new ReadOnlySpan<byte>(buffer, offset, count));
            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            return Task.FromException(ex);
        }
    }

    public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return ValueTask.FromCanceled(cancellationToken);
        }

        try
        {
            Write(buffer.Span);
            return ValueTask.CompletedTask;
        }
        catch (Exception ex)
        {
            return ValueTask.FromException(ex);
        }
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        return Read(new Span<byte>(buffer, offset, count));
    }

    public override int ReadByte()
    {
        byte b      = 0;
        int  result = Read(new Span<byte>(ref b));
        return result != 0 ? b : -1;
    }

    public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled<int>(cancellationToken);
        }

        try
        {
            return Task.FromResult(Read(new Span<byte>(buffer, offset, count)));
        }
        catch (Exception exception)
        {
            return Task.FromException<int>(exception);
        }
    }

    public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return ValueTask.FromCanceled<int>(cancellationToken);
        }

        try
        {
            return ValueTask.FromResult(Read(buffer.Span));
        }
        catch (Exception exception)
        {
            return ValueTask.FromException<int>(exception);
        }
    }

    protected override void Dispose(bool disposing)
    {
        _canRead  = false;
        _canWrite = false;
        base.Dispose(disposing);
    }

    public sealed override bool CanRead => _canRead;

    public sealed override bool CanWrite => _canWrite;

    public sealed override bool CanSeek => false;

    public sealed override long Length => throw new Exception("NotSupported");

    public sealed override long Position
    {
        get => throw new Exception("NotSupported");
        set => throw new Exception("NotSupported");
    }

    public override void Flush()
    {
    }

    public sealed override void SetLength(long value) => throw new Exception("NotSupported");

    public sealed override long Seek(long offset, SeekOrigin origin) => throw new Exception("NotSupported");

    protected void ValidateRead(byte[] buffer, int offset, int count)
    {
        ValidateCanRead();
    }

    private void ValidateCanRead()
    {
        if (!_canRead)
        {
            throw new Exception("GetReadNotSupported");
        }
    }

    protected void ValidateWrite(byte[] buffer, int offset, int count)
    {
        ValidateCanWrite();
    }

    private void ValidateCanWrite()
    {
        if (!_canWrite)
        {
            throw new Exception("WriteNotSupported");
        }
    }
}
