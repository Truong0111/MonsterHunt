using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class AutoDestructor : MonoBehaviour
{
    private enum DestroyMethod
    {
        Disable,
        PutToPool,
        Destroy
    }

    [SerializeField] private float timeDestroy = 1.5f;
    [SerializeField] private DestroyMethod destroyMethod;

    private YieldInstruction _instruction;
    private float _oldTimeDestroy;

    private void OnEnable()
    {
        StartCoroutine(DelayedDestroy());
    }

    private IEnumerator DelayedDestroy()
    {
        if (_instruction == null || _oldTimeDestroy != timeDestroy)
        {
            _instruction = new WaitForSeconds(timeDestroy);
            _oldTimeDestroy = timeDestroy;
        }

        yield return _instruction;

        AutoDestroy();
    }

    private void AutoDestroy()
    {
        switch (destroyMethod)
        {
            case DestroyMethod.Disable:
                gameObject.SetActive(false);
                break;
            case DestroyMethod.PutToPool:
                SimplePool.Despawn(gameObject);
                break;
            case DestroyMethod.Destroy:
            default:
                Destroy(gameObject);
                break;
        }
    }
}