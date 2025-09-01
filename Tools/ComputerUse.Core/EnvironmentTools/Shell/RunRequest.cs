namespace ComputerUse.Core.EnvironmentTools.Shell
{
    public class RunRequest
    {
        public const string TRUNCATED_MESSAGE = "<response clipped><NOTE>To save on context only part of this file has been shown to you. You should retry this tool after you have searched inside the file with `grep -n` in order to find the line numbers of what you are looking for.</NOTE>";
        public const int MAX_RESPONSE_LEN = 16000;
        public const int DEFAULT_TIMEOUT = 120; //Seconds

        public required string FileName{ get; set; }

        public string? Arguments { get; set; } = string.Empty;

        public int Timeout { get; set; } = DEFAULT_TIMEOUT;

        public bool IsWaitForExit { get; set; } = true;

        public bool UseShellExecute { get; set; } = false;

        public bool RedirectStandardError { get; set; } = true;

        public bool RedirectStandardOutput {  get; set; } = true;

        public override string ToString()
        {
            return $"{FileName}{(string.IsNullOrWhiteSpace(Arguments) ? string.Empty : $" {Arguments}")}";
        }
    }
}
