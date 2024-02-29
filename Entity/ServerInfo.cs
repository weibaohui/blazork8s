using System;

namespace Entity;

public class ServerInfo
{

        public string   Major        { get; set; }
        public string   Minor        { get; set; }
        public string   GitVersion   { get; set; }
        public string   GitCommit    { get; set; }
        public string   GitTreeState { get; set; }
        public DateTime BuildDate    { get; set; }
        public string   GoVersion    { get; set; }
        public string   Compiler     { get; set; }
        public string   Platform     { get; set; }

}
