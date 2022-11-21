--SCRIPT DE CREACION
BEGIN;

CREATE TABLE IF NOT EXISTS public.admin_comercio
(
    cedula integer NOT NULL,
    usuario character varying(50) COLLATE pg_catalog."default" NOT NULL,
    id_direccion integer NOT NULL,
    contrasena character varying(50) COLLATE pg_catalog."default" NOT NULL,
    nombre character varying(50) COLLATE pg_catalog."default" NOT NULL,
    apellido1 character varying(50) COLLATE pg_catalog."default" NOT NULL,
    apellido2 character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT pk_cedula_admin_comercio PRIMARY KEY (cedula)
);

CREATE TABLE IF NOT EXISTS public.admin_comercio_telefono
(
    cedula integer NOT NULL,
    telefono integer NOT NULL,
    CONSTRAINT pk_cedula_admin_comercio_telefono PRIMARY KEY (cedula, telefono)
);

CREATE TABLE IF NOT EXISTS public.admin_telefono
(
    cedula integer NOT NULL,
    telefono integer NOT NULL,
    CONSTRAINT pk_cedula_admin_telefono PRIMARY KEY (cedula, telefono)
);

CREATE TABLE IF NOT EXISTS public.administrador
(
    cedula integer NOT NULL,
    usuario character varying(50) COLLATE pg_catalog."default" NOT NULL,
    id_direccion integer NOT NULL,
    contrasena character varying(50) COLLATE pg_catalog."default" NOT NULL,
    nombre character varying(50) COLLATE pg_catalog."default" NOT NULL,
    apellido1 character varying(50) COLLATE pg_catalog."default" NOT NULL,
    apellido2 character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT pk_cedula_admin PRIMARY KEY (cedula)
);

CREATE TABLE IF NOT EXISTS public.cliente
(
    cedula integer NOT NULL,
    usuario character varying(50) COLLATE pg_catalog."default" NOT NULL,
    contrasena character varying(50) COLLATE pg_catalog."default" NOT NULL,
    nombre character varying(50) COLLATE pg_catalog."default" NOT NULL,
    apellido1 character varying(50) COLLATE pg_catalog."default" NOT NULL,
    apellido2 character varying(50) COLLATE pg_catalog."default" NOT NULL,
    telefono integer NOT NULL,
    fecha_nac date NOT NULL,
    id_direccion integer NOT NULL,
    CONSTRAINT pk_cedula_cliente PRIMARY KEY (cedula)
);

CREATE TABLE IF NOT EXISTS public.comercio_afiliado
(
    cedula integer NOT NULL,
    nombre character varying(50) COLLATE pg_catalog."default" NOT NULL,
    id_direccion integer NOT NULL,
    sinpe integer NOT NULL,
    email character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT pk_cedula_direccion PRIMARY KEY (cedula)
);

CREATE TABLE IF NOT EXISTS public.comercio_telefono
(
    cedula integer NOT NULL,
    telefono integer NOT NULL,
    CONSTRAINT pk_comercio_telefono PRIMARY KEY (cedula, telefono)
);

CREATE TABLE IF NOT EXISTS public.direccion
(
    id_direccion serial NOT NULL,
    provincia character varying(50) COLLATE pg_catalog."default" NOT NULL,
    canton character varying(50) COLLATE pg_catalog."default" NOT NULL,
    distrito character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT pk_id_direccion PRIMARY KEY (id_direccion)
);

CREATE TABLE IF NOT EXISTS public.pedido
(
    id serial NOT NULL,
    comprobante character varying(50) COLLATE pg_catalog."default" NOT NULL,
    id_direccion integer NOT NULL,
    cedula_cliente integer NOT NULL,
    id_repartidor integer NOT NULL,
    direc_exacta character varying(50) COLLATE pg_catalog."default" NOT NULL,
	entregado boolean NOT NULL,
    total integer NOT NULL,
    CONSTRAINT pk_id_pedido PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS public.producto
(
    id serial NOT NULL,
    cedula_comercio integer NOT NULL,
    precio integer NOT NULL,
    foto character varying(255) COLLATE pg_catalog."default" NOT NULL,
    nombre character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT pk_id_producto PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS public.producto_pedido
(
    id_pedido integer NOT NULL,
    id_producto integer NOT NULL,
	cantidad integer NOT NULL,
	CONSTRAINT pk_id_producto_pedido PRIMARY KEY (id_pedido, id_producto)
);

CREATE TABLE IF NOT EXISTS public.repartidor
(
    id serial NOT NULL,
    usuario character varying(50) COLLATE pg_catalog."default" NOT NULL,
    id_direccion integer NOT NULL,
    contrasena character varying(50) COLLATE pg_catalog."default" NOT NULL,
    nombre character varying(50) COLLATE pg_catalog."default" NOT NULL,
    apellido1 character varying(50) COLLATE pg_catalog."default" NOT NULL,
    apellido2 character varying(50) COLLATE pg_catalog."default" NOT NULL,
    email character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT pk_id_repartidor PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS public.solicitud_comercio
(
    cedula_admin integer NOT NULL,
    cedula_comercio integer NOT NULL,
    aceptado boolean NOT NULL,
    CONSTRAINT pk_solicitud_comercio PRIMARY KEY (cedula_admin)
);

ALTER TABLE IF EXISTS public.admin_comercio
    ADD CONSTRAINT fk_cedula_admin FOREIGN KEY (cedula)
    REFERENCES public.comercio_afiliado (cedula) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;
CREATE INDEX IF NOT EXISTS pk_cedula_admin_comercio
    ON public.admin_comercio(cedula);


ALTER TABLE IF EXISTS public.admin_comercio
    ADD CONSTRAINT fk_direccion_comercio_admin FOREIGN KEY (id_direccion)
    REFERENCES public.direccion (id_direccion) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;
CREATE INDEX IF NOT EXISTS fk_id_direccion_comercio_admin
    ON public.admin_comercio(id_direccion);


ALTER TABLE IF EXISTS public.admin_comercio_telefono
    ADD CONSTRAINT fk_cedula_admin_comercio_telefono FOREIGN KEY (cedula)
    REFERENCES public.admin_comercio (cedula) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;
CREATE INDEX IF NOT EXISTS fk_cedula_comercio_admin_telefono
    ON public.admin_comercio_telefono(cedula);


ALTER TABLE IF EXISTS public.admin_telefono
    ADD CONSTRAINT fk_cedula_admin_telefono FOREIGN KEY (cedula)
    REFERENCES public.administrador (cedula) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;
CREATE INDEX IF NOT EXISTS fk_telefono_cedula_admin
    ON public.admin_telefono(cedula);


ALTER TABLE IF EXISTS public.administrador
    ADD CONSTRAINT fk_direccion_admin FOREIGN KEY (id_direccion)
    REFERENCES public.direccion (id_direccion) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;
CREATE INDEX IF NOT EXISTS fk_id_direccion_admin
    ON public.administrador(id_direccion);


ALTER TABLE IF EXISTS public.cliente
    ADD CONSTRAINT fk_direccion_cliente FOREIGN KEY (id_direccion)
    REFERENCES public.direccion (id_direccion) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;
CREATE INDEX IF NOT EXISTS fk_id_direccion_cliente
    ON public.cliente(id_direccion);


ALTER TABLE IF EXISTS public.comercio_afiliado
    ADD CONSTRAINT fk_id_direccion FOREIGN KEY (id_direccion)
    REFERENCES public.direccion (id_direccion) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;
CREATE INDEX IF NOT EXISTS fk_direccion_comercio
    ON public.comercio_afiliado(id_direccion);


ALTER TABLE IF EXISTS public.comercio_telefono
    ADD CONSTRAINT fk_cedula_comercio_telefono FOREIGN KEY (cedula)
    REFERENCES public.comercio_afiliado (cedula) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;
CREATE INDEX IF NOT EXISTS fk_telefono_cedula
    ON public.comercio_telefono(cedula);


ALTER TABLE IF EXISTS public.pedido
    ADD CONSTRAINT fk_cedula_cliente_pedido FOREIGN KEY (cedula_cliente)
    REFERENCES public.cliente (cedula) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;
CREATE INDEX IF NOT EXISTS fk_pedido_cedula_cliente
    ON public.pedido(cedula_cliente);


ALTER TABLE IF EXISTS public.pedido
    ADD CONSTRAINT fk_direccion_pedido FOREIGN KEY (id_direccion)
    REFERENCES public.direccion (id_direccion) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;
CREATE INDEX IF NOT EXISTS fk_pedido_direccion
    ON public.pedido(id_direccion);


ALTER TABLE IF EXISTS public.pedido
    ADD CONSTRAINT fk_id_repartidor_pedido FOREIGN KEY (id_repartidor)
    REFERENCES public.repartidor (id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;
CREATE INDEX IF NOT EXISTS fk_id_pedido
    ON public.pedido(id_repartidor);


ALTER TABLE IF EXISTS public.producto
    ADD CONSTRAINT fk_cedula_comercio FOREIGN KEY (cedula_comercio)
    REFERENCES public.comercio_afiliado (cedula) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;
CREATE INDEX IF NOT EXISTS fk_cedula_comercio_producto
    ON public.producto(cedula_comercio);


ALTER TABLE IF EXISTS public.producto_pedido
    ADD CONSTRAINT fk_pedido_producto_pedido FOREIGN KEY (id_pedido)
    REFERENCES public.pedido (id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;
CREATE INDEX IF NOT EXISTS pk_producto_pedido
    ON public.producto_pedido(id_pedido);


ALTER TABLE IF EXISTS public.producto_pedido
    ADD CONSTRAINT fk_productos FOREIGN KEY (id_producto)
    REFERENCES public.producto (id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;
CREATE INDEX IF NOT EXISTS fk_producto_pedido
    ON public.producto_pedido(id_producto);


ALTER TABLE IF EXISTS public.repartidor
    ADD CONSTRAINT fk_direccion_pedido FOREIGN KEY (id_direccion)
    REFERENCES public.direccion (id_direccion) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;
CREATE INDEX IF NOT EXISTS fk_pedido_repartidor
    ON public.repartidor(id_direccion);


ALTER TABLE IF EXISTS public.solicitud_comercio
    ADD CONSTRAINT fk_cedula_admin_solicitud FOREIGN KEY (cedula_admin)
    REFERENCES public.administrador (cedula) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;
CREATE INDEX IF NOT EXISTS pk_solicitud_comercio
    ON public.solicitud_comercio(cedula_admin);


ALTER TABLE IF EXISTS public.solicitud_comercio
    ADD CONSTRAINT fk_cedula_comercio_solicutud FOREIGN KEY (cedula_comercio)
    REFERENCES public.comercio_afiliado (cedula) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;
CREATE INDEX IF NOT EXISTS fk_solicitud_cedula_comercio
    ON public.solicitud_comercio(cedula_comercio);

END;

-- VISTA CLIENTE DIRECCION
CREATE OR REPLACE VIEW public.clientedireccion
 AS
 SELECT c.cedula,
    c.usuario,
    c.contrasena,
    c.nombre,
    c.apellido1,
    c.apellido2,
    c.telefono,
    c.fecha_nac,
    d.provincia,
    d.canton,
    d.distrito,
	d.id_direccion
   FROM cliente c
     JOIN direccion d ON c.id_direccion = d.id_direccion;

ALTER TABLE public.clientedireccion
    OWNER TO postgres;

-- VISTA RESTAURANTEDIRECCION

create view restaurantedireccion 
as
select ca.cedula, ca.nombre, ca.sinpe, ca.email, d.provincia, d.canton, d.distrito 
from comercio_afiliado ca 
join direccion d 
on ca.id_direccion= d.id_direccion;

--VISTA COMERCIOSACEPTADOS

CREATE OR REPLACE VIEW public.comerciosaceptados
 AS
 SELECT
 	f.cedula,
    f.nombre,
    f.id_direccion,
    f.sinpe,
    f.email,
	d.provincia,
	d.canton,
	d.distrito
 FROM
 (SELECT ca.cedula,
    ca.nombre,
    ca.id_direccion,
    ca.sinpe,
    ca.email
   FROM comercio_afiliado ca
     JOIN solicitud_comercio sc ON ca.cedula = sc.cedula_comercio
  WHERE sc.aceptado = true) as f 
  LEFT JOIN direccion d
  ON f.id_direccion = d.id_direccion;

ALTER TABLE public.comerciosaceptados
    OWNER TO postgres;


--VISTA CONSOLIDADO_VENTAS

CREATE OR REPLACE VIEW consolidado_ventas
as 
SELECT res.cedula, count(res.cedula) as cantidad_compras, sum(res.total) as total_pagado
FROM (pedido p 
LEFT JOIN cliente c 
ON p.cedula_cliente = c.cedula) AS res
WHERE res.entregado = true
GROUP BY res.cedula;

--VISTA VENTASXAFILIADO

CREATE OR REPLACE VIEW ventasxafiliado
as 
SELECT cedula_comercio, count(cedula_comercio) as cantidad_ventas, sum(precio) as total_vendido
FROM ((pedido p
	 RIGHT JOIN producto_pedido pp
	 ON p.id = pp.id_pedido) AS productos_pedidos
	 LEFT JOIN producto pr
	 ON productos_pedidos.id_producto = pr.id ) AS total_pp_nombre
	 LEFT JOIN comercio_afiliado ca
	 ON total_pp_nombre.cedula_comercio = ca.cedula
GROUP BY cedula_comercio

--PROCEDURE CONTRASENA

CREATE OR REPLACE PROCEDURE 
cambiar_contrasenna("cedula" integer,
				   	"contrasena" character varying)

LANGUAGE SQL

AS $$

UPDATE public.cliente cl
	SET contrasena="contrasena"
	WHERE cl.cedula = "cedula";

$$;

--PROCEDURE PEDIDOAREPARTIDOR

CREATE OR REPLACE PROCEDURE 
pedido_a_repartidor("cedula_cliente" integer, 
					"comprobante" character varying,
				   	"id_repartidor" integer,
				   	"id_direccion" integer,
				    "direc_exacta" character varying,
				   	"total" integer)

LANGUAGE SQL

AS $$

INSERT INTO public.pedido(
	id, comprobante, id_direccion, cedula_cliente, id_repartidor, direc_exacta, entregado, total)
	VALUES (DEFAULT, "comprobante", "id_direccion", "cedula_cliente", "id_repartidor", "direc_exacta", false, "total");

$$;

--PROCEDURE RECEPCIONPEDIDO

CREATE OR REPLACE PROCEDURE 
recepcion_pedido("id" integer)

LANGUAGE SQL

AS $$

UPDATE public.pedido p
	SET entregado=true
	WHERE p.id = "id";

$$;

--Trigger Cliente

CREATE OR REPLACE FUNCTION befo_insert_cliente()
  RETURNS trigger AS
$$
BEGIN
NEW.nombre = LTRIM(NEW.nombre);
NEW.apellido1 = LTRIM(NEW.apellido1);
NEW.apellido2 = LTRIM(NEW.apellido2);
RETURN NEW;
END;

$$
LANGUAGE 'plpgsql';



CREATE TRIGGER befo_check_name_cliente
  BEFORE INSERT
  ON cliente
  FOR EACH ROW
  EXECUTE PROCEDURE befo_insert_cliente();

--Trigger Administrador

CREATE OR REPLACE FUNCTION befo_insert_administrador()
  RETURNS trigger AS
$$
BEGIN
NEW.nombre = LTRIM(NEW.nombre);
NEW.apellido1 = LTRIM(NEW.apellido1);
NEW.apellido2 = LTRIM(NEW.apellido2);
RETURN NEW;
END;

$$
LANGUAGE 'plpgsql';



CREATE TRIGGER befo_check_name_administrador
  BEFORE INSERT
  ON administrador
  FOR EACH ROW
  EXECUTE PROCEDURE befo_insert_administrador();