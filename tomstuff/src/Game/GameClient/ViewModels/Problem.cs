﻿namespace GameClient.ViewModels
{
    public class Problem
    {
        public string Challenge { get; set; }
        public string Id { get; set; }
        public long Size { get; set; }
        public string[] Operators { get; set; }
        public bool Solved { get; set; }
        public long TimeLeft { get; set; }
    }
}