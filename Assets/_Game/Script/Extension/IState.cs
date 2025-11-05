public interface IState<T>
{
    void OnEnter(T t);
    void OnUpdate(T t);
    void OnExit(T t);
}
