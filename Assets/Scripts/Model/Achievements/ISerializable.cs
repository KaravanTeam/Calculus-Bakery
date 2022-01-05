namespace Model.Achievements
{
    internal interface ISerializable
    {
        public void Serialize();
        public void Deserialize();
    }
}