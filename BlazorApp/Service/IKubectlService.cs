namespace BlazorApp.Service.impl;

public interface IKubectlService
{
    //调用kubectl执行命令
    public string Apply(string yaml);
}
