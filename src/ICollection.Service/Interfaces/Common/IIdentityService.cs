namespace ICollection.Service.Interfaces.Common
{
    public interface IIdentityService
    {
        public int? Id { get; }

        public string UserName { get; }

        public string ImagePath { get; }
    }
}
