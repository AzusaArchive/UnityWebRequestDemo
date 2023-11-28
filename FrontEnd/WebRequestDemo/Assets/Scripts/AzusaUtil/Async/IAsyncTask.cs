using System;

namespace Azusa.Unity.Util.Async
{
    public interface IAsyncTaskBase
    {
        /// <summary>
        /// 异步任务正在运行
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// 任务已完成
        /// </summary>
        bool IsCompleted { get; }

        /// <summary>
        /// 任务发生错误
        /// </summary>
        bool IsError { get; }

        /// <summary>
        /// 任务的错误消息
        /// </summary>
        string ErrorMessage { get; }

        /// <summary>
        /// 任务已取消
        /// </summary>
        bool IsCanceled { get; }

        /// <summary>
        /// 当发生异常时，是否要抛出
        /// </summary>
        bool ThrowOnError { get; set; }

        /// <summary>
        /// 开始异步任务
        /// </summary>
        void Start();

        /// <summary>
        /// 取消异步任务
        /// </summary>
        void Cancel();

    }
    
    /// <summary>
    /// 异步任务
    /// </summary>
    public interface IAsyncTask : IAsyncTaskBase
    {
       
        /// <summary>
        /// 添加当任务被中断时的回调函数
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        IAsyncTask WhenInterrupted(Action<IAsyncTask> callback);

        /// <summary>
        /// 添加当任务被取消时的回调函数
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        IAsyncTask WhenCanceled(Action<IAsyncTask> callback);

        /// <summary>
        /// 添加当任务成功时的回调函数
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        IAsyncTask WhenCompleted(Action<IAsyncTask> callback);

        /// <summary>
        /// 添加当任务结束时的回调函数
        /// 任务成功，取消，异常算任务结束
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        IAsyncTask WhenFinished(Action<IAsyncTask> callback);
    }

    /// <summary>
    /// 带返回值的异步任务
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IAsyncTask<TResult> : IAsyncTaskBase
    {
        /// <summary>
        /// 异步任务结果
        /// </summary>
        TResult Result { get; }
        /// <inheritdoc cref="IAsyncTask.WhenInterrupted"/>
        IAsyncTask<TResult> WhenInterrupted(Action<IAsyncTask<TResult>> callback);

        /// <inheritdoc cref="IAsyncTask.WhenCanceled"/>
        IAsyncTask<TResult> WhenCanceled(Action<IAsyncTask<TResult>> callback);

        /// <inheritdoc cref="IAsyncTask.WhenCompleted"/>
        IAsyncTask<TResult> WhenCompleted(Action<IAsyncTask<TResult>> callback);

        /// <inheritdoc cref="IAsyncTask.WhenFinished"/>
        IAsyncTask<TResult> WhenFinished(Action<IAsyncTask<TResult>> callback);
    }
}