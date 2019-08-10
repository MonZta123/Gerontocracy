namespace Morphius
{
    public class Fault
    {
        public string Name { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public Fault InnerFault { get; set; }
    }
}
