INSERT INTO public.direccion(
	id_direccion, provincia, canton, distrito)
	VALUES (DEFAULT, 'Cartago', 'Cartago', 'Cartago');

INSERT INTO public.administrador(
	cedula, usuario, id_direccion, contrasena, nombre, apellido1, apellido2)
	VALUES (305180081, 'daniel311999', 1, '1234', 'Daniel', 'Brenes', 'Gomez');