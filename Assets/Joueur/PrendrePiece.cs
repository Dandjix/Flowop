using Mono.Cecil;
using UnityEngine;

public class PrendrePiece : MonoBehaviour
{   
    [SerializeField] public TypePieceRamassee typePiece;
    public enum TypePieceRamassee {
        PieceBleue,
        PieceRouge,
        PieceVerte
    }
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (typePiece)
        {
            case TypePieceRamassee.PieceBleue:
                if (collision.CompareTag("Piece Bleue"))
                {

                    Interfacage.Instance.RamasserPiece(collision.gameObject);

                    collision.gameObject.SetActive(false);
                }
                break;
            case TypePieceRamassee.PieceRouge:
                if (collision.CompareTag("Piece Rouge"))
                {

                    Interfacage.Instance.RamasserPiece(collision.gameObject);

                    collision.gameObject.SetActive(false);
                }
                break;
            case TypePieceRamassee.PieceVerte:
                if (collision.CompareTag("Piece Verte"))
                {

                    Interfacage.Instance.RamasserPiece(collision.gameObject);

                    collision.gameObject.SetActive(false);
                }
                break;
        }
        
    }
}
