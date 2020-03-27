namespace ETModel
{
    [Event(EventIdType.EndCheckUpdate)]
    public class EventEndCheckUpdate : AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<UIComponent>().Remove(UIType.UICheckUpdate);
        }
    }
}
