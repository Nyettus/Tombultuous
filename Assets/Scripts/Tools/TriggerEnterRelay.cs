using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public interface ITriggerAcceptor
{
    public void _OnTriggerEnter2D(Collider2D collision);
    public void _OnTriggerExit2D(Collider2D collision);
}
public class TriggerEnterRelay : MonoBehaviour
{
    [SerializeField]
    [RequireInterface(typeof(ITriggerAcceptor))]
    private Object _acceptor;
    public ITriggerAcceptor Acceptor => _acceptor as ITriggerAcceptor;
    public LayerMask layer;
    void OnTriggerEnter2D(Collider2D collision)
    {
        //if ((layer.value & (1 << collision.transform.gameObject.layer)) > 0)
        if (layer.IsGameObjectInMask(collision.gameObject))
            Acceptor._OnTriggerEnter2D(collision);
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        //if ((layer.value & (1 << collision.transform.gameObject.layer)) > 0)
        if (layer.IsGameObjectInMask(collision.gameObject))
            Acceptor._OnTriggerExit2D(collision);
    }
}