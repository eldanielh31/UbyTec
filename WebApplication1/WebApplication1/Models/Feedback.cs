using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Feedback{

    public Feedback(int cedula_cliente, int id_producto, string comentario){
        this.comentario = comentario;
        this.cedula_cliente = cedula_cliente;
        this.id_producto = id_producto;
    }

    [BsonId]
    public ObjectId id { get; set; }
    public string comentario{get; set;}
    public int cedula_cliente{get; set;}
    public int id_producto{get; set;}

    public string nombre_cliente{get; set;}
}