using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[Singleton(nameof(LoadQueue), true)]
public class LoadQueue : Singleton<LoadQueue>
{
    private Queue<IEnumerator> _queueIEnumerator = new();

    private Queue<Task> _queueTask = new();

    private Queue<AsyncOperation> _queueAsync = new();

    private Queue<float> _queueTime = new();

    public Action StartAction { get; set; }

    public Action EndAction { get; set; }

    public void AddIEnumerator(IEnumerator enumerator)
    {
        _queueIEnumerator.Enqueue(enumerator);
    }

    public void AddTask(Task task)
    {
        _queueTask.Enqueue(task);
    }

    public void AddAsyncOperation(AsyncOperation async)
    {
        _queueAsync.Enqueue(async);
    }

    public void AddTime(float time)
    {
        _queueTime.Enqueue(time);
    }

    public IEnumerator CheckIEnumeratorQueue()
    {
        StartAction?.Invoke();
        if (_queueIEnumerator.Count <= 0)
        {
            yield break;
        }

        while (_queueIEnumerator.Count > 0)
        {
            var timeWait = _queueIEnumerator.Dequeue();
            yield return timeWait;
        }

        EndAction?.Invoke();
    }

    public IEnumerator CheckTaskQueue()
    {
        StartAction?.Invoke();
        if (_queueTask.Count <= 0)
        {
            yield break;
        }

        while (_queueTask.Count > 0)
        {
            var task = _queueTask.Dequeue();
            yield return new WaitUntil(() => task.IsCompleted || task.IsCanceled || task.IsFaulted);
        }

        EndAction?.Invoke();
    }

    public IEnumerator CheckAsyncQueue()
    {
        StartAction?.Invoke();
        if (_queueAsync.Count <= 0)
        {
            yield break;
        }

        while (_queueAsync.Count > 0)
        {
            var async = _queueAsync.Dequeue();

            yield return new WaitUntil(() => async == null || async.isDone || async.progress >= 1.0f);
        }

        EndAction?.Invoke();
    }

    public IEnumerator CheckTimeQueue()
    {
        StartAction?.Invoke();
        if (_queueTime.Count <= 0)
        {
            yield break;
        }

        while (_queueTime.Count > 0)
        {
            var time = _queueTime.Dequeue();

            yield return new WaitForSeconds(time);
        }

        EndAction?.Invoke();
    }

    public IEnumerator CheckAllQueue()
    {
        StartAction?.Invoke();
        yield return StartCoroutine(CheckIEnumeratorQueue());
        yield return StartCoroutine(CheckTaskQueue());
        yield return StartCoroutine(CheckAsyncQueue());
        yield return StartCoroutine(CheckTimeQueue());
        EndAction?.Invoke();
    }

    public void ClearQueue()
    {
        _queueIEnumerator = new Queue<IEnumerator>();
        _queueTask = new Queue<Task>();
        _queueAsync = new Queue<AsyncOperation>();
        _queueTime = new Queue<float>();
    }

    public void ClearAction()
    {
        StartAction = null;
        EndAction = null;
    }
}