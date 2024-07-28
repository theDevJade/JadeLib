using Exiled.API.Interfaces;

namespace TeachingStupidPeople
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; }
        public bool Debug { get; set; }
    }
}