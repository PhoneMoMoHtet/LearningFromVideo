namespace VideoLearning
{
    public class ProgramParameter
    {
        public ProgramParameter(string defaultConnectionString, string pubsConnectionString) 
        {
            this.DefaultConnectionString = defaultConnectionString;
            this.PubsConnectionString = pubsConnectionString;
        }

        public string DefaultConnectionString { get; set; }

        public string PubsConnectionString { get; set; }
    }
}
