namespace Heart_CSJDM.Helper
{
    public class GenerateTransaction
    {
        public string getTrancsactionID()
        {
                // Get the current timestamp
                long timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

                // Generate a random number
                Random random = new Random();
                int randomNumber = random.Next(100000, 999999); // Adjust the range as needed

                // Combine timestamp and random number to create the transaction ID
                string transactionId = $"{timestamp}-{randomNumber}";

                return transactionId;
            }
    }
}
