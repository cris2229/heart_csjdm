namespace Heart_CSJDM.Helper
{
    public class GenerateTransaction
    {
        public string getTrancsactionID(int clientID)
        { 
            string TransactionID = string.Empty;

            TransactionID=   DateOnly.FromDateTime(DateTime.Now).ToString("yyy-MM-dd") + "-" + clientID.ToString()  ;

            return TransactionID;
        }
    }
}
