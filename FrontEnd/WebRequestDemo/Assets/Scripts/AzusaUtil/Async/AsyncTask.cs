using System;

namespace Azusa.Unity.Util.Async
{
    public abstract class AsyncActionBase : IAsyncTaskBase
    {
        /// <summary>
        /// 创建一个异步任务
        /// </summary>
        /// <param name="asyncTask">要执行的委托函数，该委托应当是一个异步函数</param>
        /// <param name="cancelAction">当任务主动取消时运行的委托。执行该委托后<paramref name="asyncTask"/>委托应当停止</param>
        /// <param name="throwOnError">当委托出现异常时，是否抛出</param>
        /// <param name="runAtOnce">创建完任务立刻执行</param>
        /// <returns>异步任务实例</returns>
        public static IAsyncTask Create(Action<AsyncAction> asyncTask, Action cancelAction, bool throwOnError = true,
            bool runAtOnce = false)
        {
            var a = new AsyncAction(asyncTask, cancelAction, throwOnError);
            if (runAtOnce)
                a.Start();
            return a;
        }

        /// <summary>
        /// 创建一个带返回值的异步任务
        /// </summary>
        /// <param name="asyncTask">要执行的委托函数，该委托应当时一个异步函数，并且带有返回值</param>
        /// <param name="cancelAction">当任务主动取消时运行的委托。执行该委托后&lt;paramref name="asyncTask"/&gt;委托应当停止</param>
        /// <param name="throwOnError">当委托出现异常时，是否抛出</param>
        /// <param name="runAtOnce">创建完任务立刻执行</param>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <returns>异步任务实例</returns>
        public static IAsyncTask<TResult> Create<TResult>(Action<AsyncAction<TResult>> asyncTask, Action cancelAction,
            bool throwOnError = true, bool runAtOnce = false)
        {
            var a = new AsyncAction<TResult>(asyncTask, cancelAction, throwOnError);
            if (runAtOnce)
                a.Start();
            return a;
        }
        
        private bool _completed;
        private bool _error;
        private bool _canceled;
        public bool IsRunning { get; protected set; }

        public bool IsCompleted
        {
            get { return _completed; }
            protected set
            {
                if(value) 
                    IsRunning = false;
                _completed = value;
            }
        }

        public bool IsError
        {
            get { return _error; }
            protected set
            {
                if(value)
                    IsRunning = false;
                _error = value;
            }
        }

        public string ErrorMessage { get; protected set; }

        public bool IsCanceled
        {
            get { return _canceled; }
            protected set
            {
                if(value) 
                    IsRunning = false;
                _canceled = value;
            }
        }

        public bool ThrowOnError { get; set; }
        
        public abstract void Start();

        public abstract void Cancel();
    }
    
    public class AsyncAction : AsyncActionBase, IAsyncTask
    {
        private readonly Action<AsyncAction> _asyncTask;
        private readonly Action _cancelAction;

        protected event Action<IAsyncTask> OnCanceled;
        protected event Action<IAsyncTask> OnInterrupted;
        protected event Action<IAsyncTask> OnCompleted;
        protected event Action<IAsyncTask> OnFinished;

        public AsyncAction(Action<AsyncAction> asyncTask, Action cancelAction, bool throwOnError = true)
        {
            _asyncTask = asyncTask;
            _cancelAction = cancelAction;
            ThrowOnError = throwOnError;
        }

        public override void Start()
        {
            if (IsRunning)
                throw new InvalidOperationException("任务已在执行");
            try
            {
                _asyncTask(this);
            }
            catch
            {
                Error();
            }

            IsRunning = true;
        }

        public override void Cancel()
        {
            _cancelAction();
            IsCanceled = true;
            OnCanceled?.Invoke(this);
            OnFinished?.Invoke(this);
        }

        public IAsyncTask WhenInterrupted(Action<IAsyncTask> callback)
        {
            OnInterrupted += callback;
            return this;
        }

        public IAsyncTask WhenCanceled(Action<IAsyncTask> callback)
        {
            OnCanceled += callback;
            return this;
        }

        public IAsyncTask WhenCompleted(Action<IAsyncTask> callback)
        {
            OnCompleted += callback;
            return this;
        }

        public IAsyncTask WhenFinished(Action<IAsyncTask> callback)
        {
            OnFinished += callback;
            return this;
        }


        /// <summary>
        /// 完成该任务，此函数应当由任务的创建者进行调用
        /// </summary>
        public void Complete()
        {
            OnCompleted?.Invoke(this);
            IsCompleted = true;
            OnFinished?.Invoke(this);
        }

        /// <summary>
        /// 任务发生错误，此函数应当由任务的创建者进行调用
        /// </summary>
        /// <param name="message">消息</param>
        /// <exception cref="Exception">如果<see cref="AsyncActionBase.ThrowOnError"/>为真，则抛出异常</exception>
        public void Error(string message = null)
        {
            IsError = true;
            OnInterrupted?.Invoke(this);
            if (ThrowOnError)
            {
                throw new Exception(message ?? "异步任务执行失败");
            }

            OnFinished?.Invoke(this);
        }
    }

    public class AsyncAction<TResult> : AsyncActionBase,IAsyncTask<TResult>
    {
        private readonly Action<AsyncAction<TResult>> _asyncTask;
        private readonly Action _cancelAction;
       
        public event Action<IAsyncTask<TResult>> OnCanceled;
        public event Action<IAsyncTask<TResult>> OnInterrupted;
        public event Action<IAsyncTask<TResult>> OnCompleted;
        public event Action<IAsyncTask<TResult>> OnFinished;


        public TResult Result { get; private set; }

        public AsyncAction(Action<AsyncAction<TResult>> asyncTask, Action cancelAction, bool throwOnError = true)
        {
            _asyncTask = asyncTask;
            _cancelAction = cancelAction;
            ThrowOnError = throwOnError;
        }
        
        public override void Start()
        {
            if (IsRunning)
                throw new InvalidOperationException("任务已在执行");
            try
            {
                _asyncTask(this);
            }
            catch
            {
                Error();
            }

            
            IsError = false;
            ErrorMessage = string.Empty;
            IsCompleted = false;
            IsCanceled = false;
            IsRunning = false;
            
            IsRunning = true;
        }

        public override void Cancel()
        {
            _cancelAction();
            IsCanceled = true;
            OnCanceled?.Invoke(this);
            OnFinished?.Invoke(this);
        }


        public IAsyncTask<TResult> WhenInterrupted(Action<IAsyncTask<TResult>> callback)
        {
            OnInterrupted += callback;
            return this;
        }

        public IAsyncTask<TResult> WhenCanceled(Action<IAsyncTask<TResult>> callback)
        {
            OnCanceled += callback;
            return this;
        }

        public IAsyncTask<TResult> WhenCompleted(Action<IAsyncTask<TResult>> callback)
        {
            OnCompleted += callback;
            return this;
        }

        public IAsyncTask<TResult> WhenFinished(Action<IAsyncTask<TResult>> callback)
        {
            OnFinished += callback;
            return this;
        }


        /// <summary>
        /// <inheritdoc cref="AsyncAction.Complete"/>
        /// </summary>
        /// <param name="result">异步任务的结果</param>
        public void Complete(TResult result)
        {
            Result = result;
            OnCompleted?.Invoke(this);
            IsCompleted = true;
            OnFinished?.Invoke(this);
        }
        
        /// <inheritdoc cref="AsyncAction.Error"/>
        public void Error(string message = null)
        {
            IsError = true;
            ErrorMessage = message;
            OnInterrupted?.Invoke(this);
            if (ThrowOnError)
            {
                throw new Exception(message ?? "异步任务执行失败");
            }

            OnFinished?.Invoke(this);
        }
    }
}