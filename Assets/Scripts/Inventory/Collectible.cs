using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Collectible : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public int count = 1;
    [HideInInspector] public Item item;
    [Header("UI")]
    public Image img;
    public Text countText;

    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }
    public void InitialiseItem(Item newItem)
    {
        item = newItem;
        img.sprite = newItem.image;
        RefreshCount();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        img.raycastTarget = false;
        // Mets le drag en bas de la liste sur unity
        // pour pouvoir le mettre au premier plan sur l'écran
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //snap sur le slot d'inventaire
        img.raycastTarget = true;
        // assigne le drop en enfant du slot d'inventaire
        transform.SetParent(parentAfterDrag);
    }

    void Start()
    {
        InitialiseItem(item);
    }
}
