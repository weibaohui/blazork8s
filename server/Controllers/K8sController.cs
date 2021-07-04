using System.Net.Http;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class K8sController : ControllerBase
    {
        private const string CertPem = @"-----BEGIN CERTIFICATE-----
MIIDFTCCAf2gAwIBAgIIEYtq82oeWOAwDQYJKoZIhvcNAQELBQAwFTETMBEGA1UE
AxMKa3ViZXJuZXRlczAeFw0yMTA1MjMxNDA2MTRaFw0yMjA2MjAwMTU5MjRaMDYx
FzAVBgNVBAoTDnN5c3RlbTptYXN0ZXJzMRswGQYDVQQDExJkb2NrZXItZm9yLWRl
c2t0b3AwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQDByqwCY44LJmoI
paBDWWBes+oFVEJwPKijR49MBpHphbafKOKmahcdD0iukbBzn1LxRdN80nFtUZKD
geaY0MzRovzhppEx55lca7VNpC7Mez0WTybfQvTIzxf6un097EYcY72oCW/ik39S
F4+8FmNEr5LP3h0u4m4+/dexLfGcsG/MYKtcoBFuCjEjlVFWckRzTYiuoz46ZGGl
F2fS5i9gLqd8OFjUoQOwkL66qiH/AEdpxauL95o8xbYlInP820/oF4UxThsnAS/h
xkk2DNmUEuFdnfO7py2/EJs6rLNeNUgAyABjcjdeS/B/Iz+OSJD8dZLqVcW2LW6F
loj+rHLxAgMBAAGjSDBGMA4GA1UdDwEB/wQEAwIFoDATBgNVHSUEDDAKBggrBgEF
BQcDAjAfBgNVHSMEGDAWgBT6hdOODUOcTA+0cI4ZQrGstB4VozANBgkqhkiG9w0B
AQsFAAOCAQEAUury8VIwVnRHgHrPSzYSDTjMTWRl+4dCylaltiHPczzjjYv5GPZi
r7fFV2DBtIh1LVMhgknQGV7gfze41ZlJJ/wOPtQVSj4u/S+W7sW6USv+EIrhOZUk
ZsN15JlPBqgPj90VDzZBVC0FFvJMzog9AENMQuVv4JSeEQmM6BHsixEjmEKKrPRT
1Cg9NwRJX2+/W9RqSAcf+S7zTnjd+iSpU73du4FTcBia9WHQyvdcgCQXgXGlIkXQ
dgX0yS2+g31+VtI4kKmkKAC/kUBcoBZIMYK9AIuxeyXyc0c0yZGLXSXF/zuWcsNI
YrSImVDEiqWuVY9IIiF7XjJOCScxBnfP5A==
-----END CERTIFICATE-----";

        private const string EccPem = @"-----BEGIN RSA PRIVATE KEY-----
MIIEpAIBAAKCAQEAwcqsAmOOCyZqCKWgQ1lgXrPqBVRCcDyoo0ePTAaR6YW2nyji
pmoXHQ9IrpGwc59S8UXTfNJxbVGSg4HmmNDM0aL84aaRMeeZXGu1TaQuzHs9Fk8m
30L0yM8X+rp9PexGHGO9qAlv4pN/UhePvBZjRK+Sz94dLuJuPv3XsS3xnLBvzGCr
XKARbgoxI5VRVnJEc02IrqM+OmRhpRdn0uYvYC6nfDhY1KEDsJC+uqoh/wBHacWr
i/eaPMW2JSJz/NtP6BeFMU4bJwEv4cZJNgzZlBLhXZ3zu6ctvxCbOqyzXjVIAMgA
Y3I3XkvwfyM/jkiQ/HWS6lXFti1uhZaI/qxy8QIDAQABAoIBACTiqJ7DyDODGkeP
DtQC3P7pi697/DFUulxpsHKuBXPHSZ09sPYv1zpmwFTJ0iYPuz/FJJ3riD9geVyi
ivunMnTsaumgRJ6VEHGw5o9ioq20/0mRwzpXijP2hv/oIxJ4OFgK+/xR34PmwyMi
7O7F7BsUuALsqe1Ul9tRgY5tykEiVx/ygEgNX4hhd/Bhiyp86ARWvUt0y/X4nJ+c
YD2vhlWOqdAKInUl6qBCHPS1oeNNh8Qax/3RBIRPliXH3kqEirrKr1Ap/OuHXNFh
t/T9sL2bTsF8QQp+LcV8ycbz3G1M/rLerN49y5LnFxHtgzSum7huqkAdfXywcMLC
j0BprSkCgYEA6iSZEQMbV1jXsoNyOTCKbXS9xFaZ13XDvCk8A98Y3eiZQIBQbwCN
oZjli/4vXg++nU1pc7MCZhDn3hkIf/U/2WAfpcF+uyd1WCw0cqew9VG4XJjSEhHL
YmjMQwWonhuVLgrNZ8wUYU0vsnOSHQc+CYeyL0rFjfEGP5//vFQ6XucCgYEA0+HI
1GBhUDDXXgnhble7tKenK5wrkCrHF/7vdvSBLbxF6QrY9nGuqZcEcmv6PXNkvIGw
pYIqzV3REcUiM67kIPcXEKx9TZQqb1DK0pnmYl1xSpdWpSW1XtHiV+JRI1CiPFwS
U161112v9xkPFHHoFXOlqIXASkclLunKJaHZHGcCgYEAoQ2Rd+EyMk+69mBx9iKM
ZSOy0FVdNpYbj9axIFyZxzISEAryyJeR1EDOTBAIVuPvklXIHjxYfwpL4zpG3XU9
ePEkc6h32pYWohKt9Nuh3exbKt43SRSSWFuLfOJsjGyenW/yv93hethT4aSbMXpk
0rtS9jKxVqQeTy9oAgvRpqsCgYBuXYh3n3BxDc/Q6wKisE5UzpNUMve6E547EI75
fmifQxeDSSQ0UojxS3mEhFwHkEjjrAYwX/odmQWi5PVoyGuKBEreY4qtU0U7UHEl
fAa2LAgsG2KFiXvM1TS6JGexJnorSKY/CPFdKi7TXhktxBtouSGMA4di6WYj5qFm
MsTddwKBgQCzlZnQQIw9mcK5y5dcvef/N/TkGp2rakccdiicwnjdvEVvZqOfD0RQ
3lANSNXOu5b0grkMoS14Weej9bwyG1ouwiCSVaVcpD7rOiHz1LaowEw9Rcw7rU7u
nhNnQJHzAPWFZxAcQuS2HIknAHohLJ2BLeSQ1g5Qi+RezBlfqwvlYQ==
-----END RSA PRIVATE KEY-----";

        private readonly ILogger<K8sController> _logger;

        public K8sController(ILogger<K8sController> logger)
        {
            _logger = logger;
        }

        private void PodList()
        {
            var handler = new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                SslProtocols = SslProtocols.Tls12,
                ServerCertificateCustomValidationCallback = (x, y, z, m) => true
            };


            var cert = X509Certificate2.CreateFromPem(CertPem, EccPem);
            handler.ClientCertificates.Add(cert);

            var client = new HttpClient(handler);


            var httpResponseMessage =
                client.GetAsync("https://kubernetes.docker.internal:6443/api/v1/namespaces/guestbook/pods").GetAwaiter()
                    .GetResult();
            var result = httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            _logger.LogError(result);
        }
    }
}