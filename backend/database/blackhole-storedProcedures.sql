USE blackhole;

DELIMITER //
CREATE PROCEDURE validateString(INOUT string VARCHAR(150))
BEGIN
	SET string = TRIM(string);
    IF LENGTH(string) = 0 THEN
		SET string = NULL;
    END IF;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE registerUser(IN _identificator INT UNSIGNED, IN _nickname VARCHAR(30), IN _name VARCHAR(30), IN _firstLastname VARCHAR(30), IN _secondLastname VARCHAR(30), IN _email VARCHAR(350), IN _password VARCHAR(150), IN _studies TINYINT, IN _birthday DATE, IN _mexicanState TINYINT)
BEGIN
    CALL validateString(_nickname);
    CALL validateString(_name);
    CALL validateString(_firstLastname);
    CALL validateString(_secondLastname);
    CALL validateString(_email);
    INSERT INTO User(identificator, nickname, name, firstLastname, secondLastname, email, password, studies, birthday, mexicanState) VALUE (_identificator, _nickname, _name, _firstLastname, _secondLastname, _email, SHA2(_password, 512), _studies, _birthday, _mexicanState);
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE registerAchievement(OUT _identificator SMALLINT UNSIGNED, IN _title VARCHAR(50), IN _content VARCHAR(100))
BEGIN
	SET _identificator = NULL;
    CALL validateString(_title);
    CALL validateString(_content);
    INSERT INTO Achievement(title, content) VALUE (_title, _content);
    SET _identificator = @@identity;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE registerGame(OUT _identificator SMALLINT UNSIGNED, IN _time TIME, IN _incorrectAnswers TINYINT UNSIGNED, IN _mode TINYINT UNSIGNED, IN _completedModules TINYINT UNSIGNED, IN _User_identificator INT UNSIGNED)
BEGIN
	SET _identificator = NULL;
    INSERT INTO Game(time, incorrectAnswers, mode, completedModules, User_identificator) VALUE (_time, _incorrectAnswers, _mode, _completedModules, _User_identificator);
    SET _identificator = @@identity;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE registerUserAchievement(IN _Achievement_identificator TINYINT UNSIGNED, IN _User_identificator INT UNSIGNED, IN _Game_identificator BIGINT UNSIGNED)
BEGIN
    INSERT INTO UserAchievement(Achievement_identificator, User_identificator, Game_identificator) VALUE (_Achievement_identificator, _User_identificator, _Game_identificator);
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE getAvailableAchievements()
BEGIN
    SELECT identificator, title, content FROM Achievement WHERE status = 1;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE getAvailableUserAchievements(IN _User_identificator INT UNSIGNED)
BEGIN
    SELECT identificator, title, content FROM Achievement WHERE status = 1 AND identificator NOT IN (
    SELECT Achievement_identificator FROM UserAchievement WHERE User_identificator = _User_identificator);
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE getTopUsersWithGamesWonInLessTime(IN rowsLimit TINYINT UNSIGNED)
BEGIN
    SELECT user.nickname AS "Jugador", game.time AS "Tiempo", IF(game.mode = 0, "Sin guía", "Con guía") AS "Modalidad", date_format(game.register, "%d/%m/%Y") AS "Fecha" FROM Game AS game INNER JOIN User AS user ON game.User_identificator = user.identificator WHERE game.completedModules = 4 ORDER BY game.time ASC, game.register ASC, game.mode ASC, user.nickname ASC LIMIT rowsLimit;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE getTopUsersWithTheMostGamesWon(IN rowsLimit TINYINT UNSIGNED)
BEGIN
    SELECT user.nickname AS "Jugador", COUNT(game.identificator) AS Victorias, date_format(MAX(game.register), "%d/%m/%Y") AS Fecha FROM Game AS game INNER JOIN User AS user ON game.User_identificator = user.identificator WHERE game.completedModules = 4 GROUP BY user.nickname ORDER BY Victorias DESC, Fecha ASC, user.nickname ASC LIMIT rowsLimit;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE getTopUsersWithTheMostGamesPlayed(IN rowsLimit TINYINT UNSIGNED)
BEGIN
    SELECT user.nickname AS "Jugador", COUNT(game.identificator) AS Partidas, date_format(MAX(game.register), "%d/%m/%Y") AS Fecha FROM Game AS game INNER JOIN User AS user ON game.User_identificator = user.identificator GROUP BY user.nickname ORDER BY Partidas DESC, Fecha ASC, user.nickname ASC LIMIT rowsLimit;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE getTopUsersWithFewestWrongAnswers(IN rowsLimit TINYINT UNSIGNED)
BEGIN
    SELECT user.nickname AS "Jugador", SUM(game.incorrectAnswers) AS Equivocaciones, date_format(MAX(game.register), "%d/%m/%Y") AS Fecha FROM Game AS game INNER JOIN User AS user ON game.User_identificator = user.identificator GROUP BY user.nickname ORDER BY Equivocaciones ASC, Fecha ASC, user.nickname ASC LIMIT rowsLimit;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE getUserNumberOfGamesPlayed(IN _User_identificator INT UNSIGNED)
BEGIN
    SELECT COUNT(game.identificator) AS Partidas, date_format(MAX(game.register), "%d/%m/%Y") AS Fecha FROM Game AS game INNER JOIN User AS user ON game.User_identificator = user.identificator WHERE user.identificator = _User_identificator GROUP BY user.nickname ORDER BY Partidas DESC, Fecha ASC, user.nickname ASC LIMIT 1;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE getUserNumberOfGamesWon(IN _User_identificator INT UNSIGNED)
BEGIN
    SELECT COUNT(game.identificator) AS Partidas, date_format(MAX(game.register), "%d/%m/%Y") AS Fecha FROM Game AS game INNER JOIN User AS user ON game.User_identificator = user.identificator WHERE user.identificator = _User_identificator AND game.completedModules = 4 GROUP BY user.nickname ORDER BY Partidas DESC, Fecha ASC, user.nickname ASC LIMIT 1;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE getUserRecordGameTime(IN _User_identificator INT UNSIGNED)
BEGIN
    SELECT game.time AS "Tiempo", IF(game.mode = 0, "Sin guía", "Con guía") AS "Modalidad", date_format(game.register, "%d/%m/%Y") AS "Fecha" FROM Game AS game INNER JOIN User AS user ON game.User_identificator = user.identificator WHERE user.identificator = _User_identificator AND game.completedModules = 4 ORDER BY game.time ASC, game.register ASC, game.mode ASC, user.nickname ASC LIMIT 1;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE getUserAchievementsObtained(IN _User_identificator INT UNSIGNED)
BEGIN
    SELECT achievement.title AS Logro, achievement.content AS "Descripcion", date_format(game.register, "%d/%m/%Y") AS "Fecha" FROM UserAchievement AS userachiev INNER JOIN Achievement AS achievement ON userachiev.Achievement_identificator = achievement.identificator INNER JOIN Game AS game ON userachiev.Game_identificator = game.identificator WHERE userachiev.User_identificator = _User_identificator ORDER BY Logro ASC;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE getUserIncorrectAnswers(IN _User_identificator INT UNSIGNED)
BEGIN
    SELECT SUM(game.incorrectAnswers) AS "Equivocaciones", date_format(MAX(game.register), "%d/%m/%Y") AS "Fecha" FROM Game AS game INNER JOIN User AS user ON game.User_identificator = user.identificator WHERE user.identificator = _User_identificator GROUP BY user.identificator LIMIT 1;
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE getUserStatistics(IN _User_identificator INT UNSIGNED, IN rowsLimit TINYINT UNSIGNED)
BEGIN
	-- Global
    CALL getTopUsersWithGamesWonInLessTime(10);
    CALL getTopUsersWithTheMostGamesWon(10);
    CALL getTopUsersWithTheMostGamesPlayed(10);
    CALL getTopUsersWithFewestWrongAnswers(10);
    -- Personal
    CALL getUserNumberOfGamesPlayed(_User_identificator);
    CALL getUserNumberOfGamesWon(_User_identificator);
    CALL getUserRecordGameTime(_User_identificator);
    CALL getUserAchievementsObtained(_User_identificator);
    CALL getUserIncorrectAnswers(_User_identificator);
END//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE getStatistics()
BEGIN
	SELECT user.nickname AS Jugador, IFNULL(COUNT(game.identificator), 0) AS "Partidas", IFNULL(Victorias.Partidas, 0) AS "Victorias", IFNULL(Derrotas.Partidas, 0) AS "Derrotas", IFNULL(SUM(game.incorrectAnswers), 0) AS "Equivocaciones", IFNULL(SinGuia.Partidas, 0) AS "SinGuia", IFNULL(ConGuia.Partidas, 0) AS "ConGuia", IFNULL(MejorTiempo.Tiempo, "NA") AS "MejorTiempo", IFNULL(Logros.Insignias, 0) AS "Logros", IFNULL(date_format(MAX(game.register), "%d/%m/%Y"), "NA") AS "UltimaPartida" 
	FROM Game AS game INNER JOIN User AS user ON game.User_identificator = user.identificator
	LEFT JOIN (SELECT user.nickname AS Jugador, COUNT(game.identificator) AS Partidas FROM Game AS game INNER JOIN User AS user ON game.User_identificator = user.identificator WHERE game.completedModules >= 4 GROUP BY user.nickname) AS Victorias ON user.nickname = Victorias.Jugador
	LEFT JOIN (SELECT user.nickname AS Jugador, COUNT(game.identificator) AS Partidas FROM Game AS game INNER JOIN User AS user ON game.User_identificator = user.identificator WHERE game.completedModules < 4 GROUP BY user.nickname) AS Derrotas ON user.nickname = Derrotas.Jugador
	LEFT JOIN (SELECT user.nickname AS Jugador, COUNT(game.identificator) AS Partidas FROM Game AS game INNER JOIN User AS user ON game.User_identificator = user.identificator WHERE game.mode = 0 GROUP BY user.nickname) AS SinGuia ON user.nickname = SinGuia.Jugador
	LEFT JOIN (SELECT user.nickname AS Jugador, COUNT(game.identificator) AS Partidas FROM Game AS game INNER JOIN User AS user ON game.User_identificator = user.identificator WHERE game.mode = 1 GROUP BY user.nickname) AS ConGuia ON user.nickname = ConGuia.Jugador
    LEFT JOIN (SELECT user.nickname AS Jugador, MIN(game.time) AS Tiempo FROM Game AS game INNER JOIN User AS user ON game.User_identificator = user.identificator WHERE game.completedModules = 4 GROUP BY user.nickname) AS MejorTiempo ON user.nickname = MejorTiempo.Jugador
	LEFT JOIN (SELECT UserAchievement.User_identificator AS JugadorID, COUNT(UserAchievement.Achievement_identificator) AS Insignias FROM UserAchievement AS UserAchievement GROUP BY UserAchievement.User_identificator) AS Logros ON user.identificator = Logros.JugadorID
	GROUP BY user.identificator ORDER BY Jugador ASC;
END//
DELIMITER ;
