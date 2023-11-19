using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Block : MonoBehaviour
{
    [SerializeField] private GameObject _Shadow;

    [SerializeField] private AnimationClip _destroyClip;
    [SerializeField] private GameObject _destroyEffect;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Place(Cell cell)
    {
        cell.OwnedBlock = this;

        transform.SetParent(cell.transform);
        transform.localPosition = Vector3.zero;

        SetActiveShadow(false);
    }

    public void SetActiveShadow(bool value)
    {
        _Shadow.SetActive(value);
    }

    public IEnumerator Destroy()
    {
        _animator.SetTrigger("Destroy");

        GameObject effect = Instantiate(_destroyEffect);
        effect.transform.position = transform.position;

        yield return new WaitForSeconds(_destroyClip.length);

        Destroy(gameObject);
    }
}
