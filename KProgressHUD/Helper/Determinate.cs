namespace KProgressHUD
{
    public interface Determinate
    {
        void SetMax(int max);
        void SetProgress(int progress);
    }

    public interface Indeterminate
    {
        void SetAnimationSpeed(float scale);
    }
}