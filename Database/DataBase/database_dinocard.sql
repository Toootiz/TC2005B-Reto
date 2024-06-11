DROP SCHEMA IF EXISTS dinocard_db;
CREATE SCHEMA dinocard_db;
USE dinocard_db;

-- Crear tabla para `habilidad`
CREATE TABLE habilidad (
    id_habilidad INT NOT NULL AUTO_INCREMENT,
    descripcion TEXT,
    PRIMARY KEY (id_habilidad)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Crear tabla para `habilidadData`
CREATE TABLE habilidadData (
    id_habilidad INT,
    venenodmg INT,
    quemadodmg INT,
    sangradodmg INT,
    mordidadmg INT,
    colatazodmg INT,
    boostvida INT,
    boostataquedmg INT,
    boostcosto INT,
    duracion INT,
    FOREIGN KEY (id_habilidad) REFERENCES habilidad(id_habilidad)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Crear tabla para `carta`
CREATE TABLE carta (
    id_carta INT NOT NULL AUTO_INCREMENT,
    nombre VARCHAR(255),
    puntos_de_vida INT,
    puntos_de_ataque INT,
    coste_en_elixir INT,
    habilidad INT,
    PRIMARY KEY (id_carta),
    FOREIGN KEY (habilidad) REFERENCES habilidad(id_habilidad)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Crear tabla para `jugador`
CREATE TABLE jugador (
    id_jugador INT NOT NULL AUTO_INCREMENT,
    nombre VARCHAR(255),
    contrasena VARCHAR(255),
    partidas_ganadas INT,
    partidas_perdidas INT,
    fecha_creacion TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    fecha_modificacion TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (id_jugador)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Crear tabla para `deck`
CREATE TABLE deck (
    id_deck INT NOT NULL AUTO_INCREMENT,
    id_jugador INT,
    nombre_deck VARCHAR(255),
    descripcion_deck VARCHAR(255),
    id_carta1 INT,
    id_carta2 INT,
    id_carta3 INT,
    id_carta4 INT,
    id_carta5 INT,
    id_carta6 INT,
    id_carta7 INT,
    id_carta8 INT,
    id_carta9 INT,
    id_carta10 INT,
    fecha_de_creacion TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    fecha_modificacion TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (id_deck),
    FOREIGN KEY (id_jugador) REFERENCES jugador(id_jugador)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Crear tabla para `partida`
CREATE TABLE partida (
    id_partida INT,
    id_jugador INT,
    id_ganador INT,
    id_perdedor INT,
    cantidad_turnos INT,
    FOREIGN KEY (id_jugador) REFERENCES jugador(id_jugador)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


-- VISTA CARTAS Y HABILIDADES 
CREATE VIEW vista_detalles_cartas AS
SELECT
    c.id_carta,
    c.nombre AS nombre_carta,
    c.puntos_de_vida,
    c.puntos_de_ataque,
    c.coste_en_elixir,
    h.descripcion AS habilidad
FROM
    carta c
    LEFT JOIN habilidad h ON c.habilidad = h.id_habilidad;


-- VISTA INFORMACION JUGADORES Y ESTADISTICAS
CREATE VIEW vista_estadisticas_jugadores AS
SELECT
    j.id_jugador,
    j.nombre AS nombre_jugador,
    j.partidas_ganadas,
    j.partidas_perdidas,
    j.fecha_creacion,
    j.fecha_modificacion
FROM
    jugador j;
    
    
-- VISTA PARA VER LOS DECKS Y SUS CARTAS

 



-- VISTA PARA OBTENER PARTIDAS Y RESULTADOS 
CREATE VIEW vista_resultados_partidas AS
SELECT
    p.id_partida,
    p.id_jugador,
    j.nombre AS nombre_jugador,
    p.id_ganador,
    jg.nombre AS nombre_ganador,
    p.id_perdedor,
    jp.nombre AS nombre_perdedor,
    p.cantidad_turnos
FROM
    partida p
    LEFT JOIN jugador j ON p.id_jugador = j.id_jugador
    LEFT JOIN jugador jg ON p.id_ganador = jg.id_jugador
    LEFT JOIN jugador jp ON p.id_perdedor = jp.id_jugador;

CREATE VIEW carta_habilidad_detalle AS
SELECT 
	c.id_carta,
    c.Nombre,
    c.Puntos_de_Vida,
    c.Puntos_de_ataque,
    c.Coste_en_elixir,
    h.descripcion,
    h.id_habilidad,
    hd.venenodmg,
    hd.quemadodmg,
    hd.sangradodmg,
    hd.mordidadmg,
    hd.colatazodmg,
    hd.boostvida,
    hd.boostataquedmg,
    hd.boostcosto,
    hd.duracion
FROM 
    carta c
JOIN 
    habilidad h ON c.Habilidad = h.id_habilidad
JOIN 
    habilidadData hd ON h.id_habilidad = hd.id_habilidad;


CREATE VIEW vista_cartas_habilidades_por_deck AS
SELECT
    d.id_deck,
    c.id_carta,
    c.Nombre,
    c.Puntos_de_Vida,
    c.Puntos_de_ataque,
    c.Coste_en_elixir,
    h.descripcion,
    h.id_habilidad,
    hd.venenodmg,
    hd.quemadodmg,
    hd.sangradodmg,
    hd.mordidadmg,
    hd.colatazodmg,
    hd.boostvida,
    hd.boostataquedmg,
    hd.boostcosto,
    hd.duracion
FROM
    deck d
    JOIN carta c ON d.id_carta1 = c.id_carta OR d.id_carta2 = c.id_carta OR d.id_carta3 = c.id_carta OR
                  d.id_carta4 = c.id_carta OR d.id_carta5 = c.id_carta OR d.id_carta6 = c.id_carta OR
                  d.id_carta7 = c.id_carta OR d.id_carta8 = c.id_carta OR d.id_carta9 = c.id_carta OR
                  d.id_carta10 = c.id_carta
    LEFT JOIN habilidad h ON c.habilidad = h.id_habilidad
    LEFT JOIN habilidadData hd ON h.id_habilidad = hd.id_habilidad;
    
    SELECT * FROM vista_cartas_habilidades_por_deck WHERE id_deck = 1;
    
    SELECT * FROM carta_habilidad_detalle ORDER BY id_carta;
select
    id_carta,
    Nombre,
    Puntos_de_Vida,
    Puntos_de_ataque,
    Coste_en_elixir,
    id_habilidad,
    descripcion,
    venenodmg,
    quemadodmg,
    sangradodmg,
    mordidadmg,
    colatazodmg,
    boostvida,
    boostataquedmg,
    boostcosto,
    duracion
FROM vista_cartas_habilidades_por_deck
WHERE id_deck = 1;

