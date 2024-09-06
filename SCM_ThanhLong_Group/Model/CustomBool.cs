    namespace SCM_ThanhLong_Group.Model
{
    public class CustomBool
    {
        public int Value { get; set; }

        public static implicit operator CustomBool(int value)
        {
            return new CustomBool { Value = value };
        }

        public static implicit operator bool(CustomBool customBool)
        {
            return customBool.Value == 1;
        }
    }
}
