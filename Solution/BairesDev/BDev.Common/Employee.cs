namespace BDev.Common
{
    public class Employee
    {
        /// <summary>
        /// Primary Key from the database
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Employee's Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Employee's Type
        /// </summary>
        public EmployeeType Type { get; set; }
        /// <summary>
        /// Employee's Image byte Array, 
        /// This way of saving an image may be inefficient because it'll take the db server space to store the content on bytes,
        /// Also takes more to save and load.
        /// We left it this way so it could meet the requirements but i strongly recommend to save only the url of the image
        /// </summary>
        public byte[] Image { get; set; }
    }
}
