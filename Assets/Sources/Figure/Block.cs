using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Block : MonoBehaviour
{
    [SerializeField] private GameObject _Shadow;

    [SerializeField] private AnimationClip _destructionClip;
    [SerializeField] private GameObject _destructionEffectPrefab;

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

    public void StartDestruction()
    {
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        _animator.SetTrigger("Destruction");

        GameObject effect = Instantiate(_destructionEffectPrefab);
        effect.transform.position = transform.position;

        yield return new WaitForSeconds(_destructionClip.length);

        Destroy(gameObject);
    }
}
