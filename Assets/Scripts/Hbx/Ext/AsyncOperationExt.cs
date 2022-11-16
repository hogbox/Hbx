using System;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Hbx.Ext
{
    public static class AsyncOperationExt
    {
        public static TaskAwaiter GetAwaiter(this AsyncOperation asyncOp)
        {
            var tcs = new TaskCompletionSource<AsyncOperation>();
            asyncOp.completed += operation => { tcs.SetResult(operation); };
            return ((Task)tcs.Task).GetAwaiter();
        }
    }
}
