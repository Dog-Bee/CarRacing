public class GameplayStateChangedSignal
{
    public GamePlayState GamePlayState { get; }

    public GameplayStateChangedSignal(GamePlayState gamePlayState)
    {
        GamePlayState = gamePlayState;
    }
}
