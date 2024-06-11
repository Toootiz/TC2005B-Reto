USE dinocard_db;

-- Insertar habilidades en la tabla habilidad
INSERT INTO habilidad (descripcion) VALUES 
(''),
('Da un mordisco que causa veneno'),
('Lanza una llamarada de fuego'),
('Pega una mordida y deja sangrando'),
('Pega una mordida fuerte haciendo uno de daño extra'),
('Pega fuertemente con su cola haciendo uno de daño extra'),
('Da un mordisco que causa veneno '),
('Lanza una llamarada de fuego'),
('Pega una mordida y deja sangrando'),
('Pega una mordida fuerte haciendo dos de daño extra'),
('Pega fuertemente con su cola haciendo dos de daño extra'),
('Da un mordisco que causa veneno'),
('Lanza una llamarada de fuego'),
('Pega una mordida y deja sangrando'),
('Pega una mordida fuerte haciendo tres de daño extra'),
('Pega fuertemente con su cola haciendo tres de daño extra'),
('Hace un rugido el cual aumenta la vida de una carta'),
('Hace un rugido el cual aumenta el ataque de un dinosaurio'),
('Disminuye el coste de una carta');


-- Insertar datos de habilidades
INSERT INTO habilidadData (id_habilidad, venenodmg, quemadodmg, sangradodmg, mordidadmg, colatazodmg, boostvida, boostataquedmg, boostcosto, duracion) VALUES
(1, 0, 0, 0, 0, 0, 0, 0, 0, 0), -- Nada
(2, 1, 0, 0, 0, 0, 0, 0, 0, 1), -- Veneno
(3, 0, 1, 0, 0, 0, 0, 0, 0, 1), -- Quemado
(4, 0, 0, 1, 0, 0, 0, 0, 0, 1), -- Sangrado
(5, 0, 0, 0, 1, 0, 0, 0, 0, 1), -- Mordida
(6, 0, 0, 0, 0, 1, 0, 0, 0, 1), -- Colatazo
(7, 2, 0, 0, 0, 0, 0, 0, 0, 2), -- Veneno
(8, 0, 2, 0, 0, 0, 0, 0, 0, 2), -- Quemado
(9, 0, 0, 2, 0, 0, 0, 0, 0, 2), -- Sangrado
(10, 0, 0, 0, 2, 0, 0, 0, 0, 1), -- Mordida
(11, 0, 0, 0, 0, 2, 0, 0, 0, 1), -- Colatazo
(12, 3, 0, 0, 0, 0, 0, 0, 0, 3), -- Veneno
(13, 0, 3, 0, 0, 0, 0, 0, 0, 3), -- Quemado
(14, 0, 0, 3, 0, 0, 0, 0, 0, 3), -- Sangrado
(15, 0, 0, 0, 3, 0, 0, 0, 0, 1), -- Mordida
(16, 0, 0, 0, 0, 3, 0, 0, 0, 1), -- Colatazo
(17, 0, 0, 0, 0, 0, 1, 0, 0, 1), -- Aumenta la vida de una carta
(18, 0, 0, 0, 0, 0, 0, 1, 0, 1), -- Aumenta el ataque de un dinosaurio
(19, 0, 0, 0, 0, 0, 0, 0, 1, 1); -- Disminuye el coste de una carta



-- Insertar cartas con los datos proporcionados
INSERT INTO carta (Nombre, Puntos_de_Vida, Puntos_de_ataque, Coste_en_elixir, Habilidad) VALUES
('T-Rex', 18, 16, 10, 12),
('Compy', 1, 1, 1, 1),
('Spinosaurus', 12, 10, 7, 7),
('Argentavis', 8, 4, 4, 1),
('Brontosaurus', 22, 18, 10, 13),
('Stegosaurus', 10, 3, 6, 1),
('Ankylosaurus', 3, 5, 4, 1),
('Allosaurus', 7, 6, 6, 2),
('Carnotaurus', 6, 5, 4, 1),
('Pteranodon', 5, 6, 4, 1),
('Triceratops', 20, 17, 10, 14),
('Velociraptor', 8, 5, 5, 1),
('Mosasaurus', 9, 15, 7, 7),
('Megalodon', 14, 11, 7, 8),
('Therizinosaurus', 13, 10, 8, 9),
('Giganotosaurus', 11, 15, 8, 10),
('Diplodocus', 16, 19, 10, 15),
('Parasaurolophus', 7, 4, 4, 1),
('Sabertooth', 9, 5, 5, 18),
('Woolly Mammoth', 12, 4, 5, 1),
('Direwolf', 8, 5, 5, 1),
('Baryonyx', 8, 5, 5, 1),
('Dodo', 3, 2, 1, 1),
('Kaprosuchus', 17, 20, 7, 11),
('Oviraptor', 2, 1, 3, 1),
('Giganoto', 19, 16, 10, 16),
('Gallimimus', 6, 3, 4, 3),
('Pulmonoscorpius', 22, 18, 10, 17),
('Archaeopteryx', 2, 1, 3, 1),
('Wyvern', 10, 12, 9, 9);


INSERT INTO jugador (nombre, contrasena, partidas_ganadas, partidas_perdidas) VALUES 
("Mesc", "1234", 2, 3),
("Jose", "1234", 2, 1),
("Gatuno7000","1234", 2, 1),
("Enemigo", "Enemigo", 1, 1);


-- Insertar decks de los jugaodres y de los que puede usar el enemigo
INSERT INTO deck (id_jugador, nombre_deck, descripcion_deck, id_carta1, id_carta2, id_carta3, id_carta4, id_carta5, id_carta6, id_carta7, id_carta8, id_carta9, id_carta10) VALUES
(1,"Deck Chispa", "Para jugar de chill", 1, 19, 14, 4, 5, 16, 7, 8, 9, 10),
(2, "Deck Uno", "Para jugar mucho", 1, 19, 4, 14, 8, 10, 12, 14, 16, 18),
(3,"Deck Dos", "Para jugar rapido", 2, 19, 4, 14, 8, 10, 15, 18, 16, 21),
(4,"Deck Enemigo 1", "Primer deck del enemigo", 10, 11, 12, 13, 14, 15, 16, 17, 18, 19),
(4, "Deck Enemigo 2", "Segundo deck del enemigo", 1, 2, 3, 4, 5, 6, 7, 8, 9, 10),
(4, "Deck Enemigo 3", "Tercer deck del enemigo", 11, 12, 13, 14, 15, 16, 17, 18, 19, 20),
(4, "Deck Enemigo 4", "Cuarto deck del enemigo", 21, 22, 23, 24, 25, 26, 27, 28, 1, 2),
(4, "Deck Enemigo 5", "Quinto deck del enemigo", 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);

