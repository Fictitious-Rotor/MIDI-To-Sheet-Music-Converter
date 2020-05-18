namespace MIDI_To_Sheet_Music_Converter
{
    class ReportMessage
    {
        public string Message { get; }

        public bool IsError { get; }

        public static readonly ReportMessage NoError = new ReportMessage();

        private ReportMessage()
        {
            this.IsError = false;
            this.Message = string.Empty;
        }

        public ReportMessage(string message)
        {
            this.Message = message;
            this.IsError = true;
        }
    }
}
