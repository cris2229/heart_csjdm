namespace Heart_CSJDM.Model
{
    public class Appointment
    {
        public int AppointmentID { get; set; }

        public DateTime DateofAppointment { get; set; }

        public string TypeOfServices { get; set; }

        public string Services { get; set; }

        public string AssignedTo { get; set; }

        public string Status { get; set; }

        public string Remarks { get; set; }

        public DateTime DateAdd { get; set; }

        public DateTime DateUpdated { get; set; }

        public string TransactionID { get; set; }
        public int ClientID { get; set; }
        public bool IsCheck { get; set; }
        public string ClientName { get; set; }
    }
}
