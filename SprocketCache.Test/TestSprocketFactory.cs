namespace SprocketCache.Test
{
    public class TestSprocketFactory : ISprocketFactory
    {
        public Sprocket CreateSprocket()
        {
            return new Sprocket();
        }
    }
}
