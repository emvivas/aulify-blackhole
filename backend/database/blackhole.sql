-- MySQL Workbench Forward Engineering

-- -----------------------------------------------------
-- Schema blackhole
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema blackhole
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `blackhole` DEFAULT CHARACTER SET utf8mb4 ;
USE `blackhole` ;

-- -----------------------------------------------------
-- Table `blackhole`.`User`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `blackhole`.`User` (
  `identificator` INT UNSIGNED NOT NULL,
  `nickname` VARCHAR(30) NULL,
  `name` VARCHAR(30) NULL,
  `firstLastname` VARCHAR(30) NULL,
  `secondLastname` VARCHAR(30) NULL,
  `email` VARCHAR(350) NULL,
  `password` VARCHAR(150) NULL,
  `studies` TINYINT UNSIGNED NULL,
  `birthday` DATE NULL,
  `mexicanState` TINYINT UNSIGNED NULL,
  `register` DATETIME NULL DEFAULT NOW(),
  `status` TINYINT UNSIGNED NULL DEFAULT 1,
  PRIMARY KEY (`identificator`),
  UNIQUE INDEX `identificator_UNIQUE` (`identificator` ASC) VISIBLE,
  UNIQUE INDEX `nickname_UNIQUE` (`nickname` ASC) VISIBLE,
  UNIQUE INDEX `email_UNIQUE` (`email` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `blackhole`.`Achievement`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `blackhole`.`Achievement` (
  `identificator` TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `title` VARCHAR(50) NOT NULL,
  `content` VARCHAR(100) NOT NULL,
  `register` DATETIME NOT NULL DEFAULT NOW(),
  `status` TINYINT UNSIGNED NOT NULL DEFAULT 1,
  PRIMARY KEY (`identificator`),
  UNIQUE INDEX `identificator_UNIQUE` (`identificator` ASC) VISIBLE,
  UNIQUE INDEX `title_UNIQUE` (`title` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `blackhole`.`Game`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `blackhole`.`Game` (
  `identificator` BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `time` TIME NOT NULL,
  `incorrectAnswers` TINYINT UNSIGNED NOT NULL,
  `mode` TINYINT UNSIGNED NOT NULL,
  `completedModules` TINYINT UNSIGNED NOT NULL,
  `register` DATETIME NOT NULL DEFAULT NOW(),
  `User_identificator` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`identificator`),
  UNIQUE INDEX `identificator_UNIQUE` (`identificator` ASC) VISIBLE,
  INDEX `fk_Game_User1_idx` (`User_identificator` ASC) VISIBLE,
  CONSTRAINT `fk_Game_User1`
    FOREIGN KEY (`User_identificator`)
    REFERENCES `blackhole`.`User` (`identificator`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `blackhole`.`UserAchievement`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `blackhole`.`UserAchievement` (
  `Achievement_identificator` TINYINT UNSIGNED NOT NULL,
  `User_identificator` INT UNSIGNED NOT NULL,
  `Game_identificator` BIGINT UNSIGNED NOT NULL,
  PRIMARY KEY (`Achievement_identificator`, `User_identificator`),
  INDEX `fk_GameAchievement_Achievement1_idx` (`Achievement_identificator` ASC) VISIBLE,
  INDEX `fk_UserAchievement_User1_idx` (`User_identificator` ASC) VISIBLE,
  INDEX `fk_UserAchievement_Game1_idx` (`Game_identificator` ASC) VISIBLE,
  CONSTRAINT `fk_GameAchievement_Achievement1`
    FOREIGN KEY (`Achievement_identificator`)
    REFERENCES `blackhole`.`Achievement` (`identificator`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_UserAchievement_User1`
    FOREIGN KEY (`User_identificator`)
    REFERENCES `blackhole`.`User` (`identificator`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_UserAchievement_Game1`
    FOREIGN KEY (`Game_identificator`)
    REFERENCES `blackhole`.`Game` (`identificator`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;
