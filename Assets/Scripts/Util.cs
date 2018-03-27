namespace Assets.Scripts
{
    public class Util
    {
        public static float ToMs(float kmh)
        {
            return kmh * 1000 / 3600;
        }
        public static float ToKmH(float ms)
        {
            return ms * 3600 / 1000;
        }

        public static float CalcImpulse(float massKg, float speedMs)
        {
            return massKg * speedMs;
        }

        public static float CalcSpeed(float impulse, float massKg)
        {
            return impulse / massKg;
        }

    }
}
