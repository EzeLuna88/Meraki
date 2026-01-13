namespace MPP
{
    public static class SQLQueryConstants
    {
        public const string Cliente_Insertar = @"INSERT INTO clientes
                            (codigo, nombre, direccion, localidad, telefono, telefono_alternativo, horario_de_apertura, horario_de_cierre)
                            VALUES (@codigo, @nombre, @direccion, @localidad, @telefono, @telefonoAlternativo, @apertura, @cierre)";

        public const string CompraTemporal_EliminarPorCliente = "DELETE FROM compra_producto_temporal WHERE id_cliente = @id_cliente";

        public const string CompraTemporal_Insertar = @"INSERT INTO compra_producto_temporal
                                (id_cliente, id_producto, cantidad, precio_total)
                                VALUES (@id_cliente, @id_producto, @cantidad, @precio_total)";

        public const string Cliente_Listar = @"SELECT codigo, nombre, direccion, localidad, telefono, telefono_alternativo,
                            horario_de_apertura, horario_de_cierre, comentarios
                     FROM clientes";

        public const string CompraTemporal_ObtenerPorCliente = @"
        SELECT
            cpt.id_producto,
            cpt.cantidad,
            cpt.precio_total,
            p.stock_id,
            p.unidad,
            p.precio_mayorista,
            p.precio_minorista,
            p.tipo,
            s.nombre,
            s.medida,
            s.tipo_medida,
            s.cantidad_actual,
            s.cantidad_reservada
        FROM compra_producto_temporal cpt
        JOIN producto p ON cpt.id_producto = p.id
        LEFT JOIN stock s ON p.stock_id = s.id
        WHERE cpt.id_cliente = @idCliente;";

        public const string Producto_ObtenerPorId = @"
        SELECT id, nombre, unidad, precio_mayorista, precio_minorista, tipo
        FROM producto
        WHERE id = @idCombo";

        public const string ProductoStock_ObtenerPorProductoId = @"
        SELECT stock_id, cantidad
        FROM producto_stock
        WHERE producto_id = @idCombo";

        public const string Stock_ObtenerPorId = @"
        SELECT id, nombre, medida, tipo_medida, cantidad_actual, cantidad_reservada
        FROM stock
        WHERE id = @id";

        public const string Cliente_Eliminar = "DELETE FROM clientes WHERE codigo = @codigo";

        public const string Cliente_Modificar = @"UPDATE clientes
                     SET nombre = @nombre,
                         direccion = @direccion,
                         localidad = @localidad,
                         telefono = @telefono,
                         telefono_alternativo = @telefonoAlternativo,
                         horario_de_apertura = @apertura,
                         horario_de_cierre = @cierre
                     WHERE codigo = @codigo";

        public const string Cliente_ActualizarComentarios = @"UPDATE clientes
                        SET comentarios = @comentarios
                        WHERE codigo = @codigo";
    }
}
