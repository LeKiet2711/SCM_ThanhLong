namespace SCM_ThanhLong_Group.Model
{
    public class ProfileInformation
    {
        //Class này chứa thông tin thuộc tính profile đã tạo trong database Oracle
        public string RESOURCE_NAME { get; set; }
        public string LIMIT { get; set; }
        public ProfileInformation() { }

        public ProfileInformation(string rESOURCE_NAME, string lIMIT)
        {
            RESOURCE_NAME = rESOURCE_NAME;
            LIMIT = lIMIT;
        }
    }
}
    