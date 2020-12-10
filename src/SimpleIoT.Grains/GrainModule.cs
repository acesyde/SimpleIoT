using System.Reflection;

namespace SimpleIoT.Grains
{
    public static class GrainModule
    {
        public static Assembly Assembly => typeof(GrainModule).Assembly;
    }
}
