using Android.Content;

namespace Tandis.Droid
{
    class Intent_Android : MainActivity, IIntent
    {
        public string GetExtra(string name)
        {
            Intent i = new Intent(this.BaseContext,typeof(MainActivity));
            return i.GetStringExtra(name);
        }

        public bool HasExtra(string name)
        {
            Intent i = new Intent(this.BaseContext,typeof(MainActivity));
            return  i.HasExtra(name);
        }
    }
}