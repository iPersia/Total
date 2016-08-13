namespace Nzl.Smth.Interfaces
{
    using Nzl.Smth.Datas;

    /// <summary>
    /// 
    /// </summary>
    public interface IContainsThread
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        Thread GetSavedThread(string tid);
    }
}
