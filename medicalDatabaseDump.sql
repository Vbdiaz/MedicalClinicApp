-- MySQL dump 10.13  Distrib 8.0.32, for Win64 (x86_64)
--
-- Host: medicaldatabase3380.mysql.database.azure.com    Database: medicalclinicdb2
-- ------------------------------------------------------
-- Server version	8.0.32

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `appointment`
--

DROP TABLE IF EXISTS `appointment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `appointment` (
  `AppointmentID` int NOT NULL AUTO_INCREMENT,
  `doctorID` int NOT NULL,
  `patientID` int DEFAULT NULL,
  `Approval` tinyint(1) DEFAULT '0',
  `reportID` int DEFAULT NULL,
  `officeID` int NOT NULL,
  `Referral` tinyint(1) DEFAULT '0',
  `AppointmentDate` date DEFAULT NULL,
  `AppointmentTime` time DEFAULT NULL,
  `PATIENT_CONFIRM` tinyint(1) DEFAULT NULL,
  `archive` tinyint(1) DEFAULT '0',
  `cancellation_reason` varchar(255) DEFAULT NULL,
  `nurseID` int DEFAULT NULL,
  PRIMARY KEY (`AppointmentID`),
  KEY `doctor_idx` (`doctorID`),
  KEY `patient_idx` (`patientID`),
  KEY `officeID` (`officeID`),
  KEY `reportID_foreignkey` (`reportID`),
  CONSTRAINT `APPOINTMENT_ibfk_1` FOREIGN KEY (`officeID`) REFERENCES `office` (`officeID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `doctorID_FK` FOREIGN KEY (`doctorID`) REFERENCES `doctor` (`DoctorID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `patientID_FK` FOREIGN KEY (`patientID`) REFERENCES `patients` (`PatientID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `reportID_foreignkey` FOREIGN KEY (`reportID`) REFERENCES `visit_details` (`reportID`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=30087 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `appointment`
--

LOCK TABLES `appointment` WRITE;
/*!40000 ALTER TABLE `appointment` DISABLE KEYS */;
INSERT INTO `appointment` VALUES (30000,1000,4000,1,NULL,2,0,'2023-04-30','10:00:00',0,1,'Denied by PCP.',NULL),(30001,1001,4001,0,20027,1,1,'2023-04-30','10:30:00',0,1,'Canceled by Doctor.',2001),(30002,1002,4002,0,NULL,1,1,'2023-03-30','11:00:00',NULL,1,NULL,2001),(30003,1003,4003,1,NULL,1,1,'2023-04-30','11:30:00',1,1,'Canceled by Patient.',NULL),(30004,1004,4003,1,NULL,1,0,'2023-04-30','12:00:00',0,1,'Canceled by Patient.',NULL),(30005,1005,4004,0,20025,2,1,'2023-04-30','12:30:00',NULL,0,NULL,2006),(30006,1006,4005,0,NULL,2,1,'2023-04-28','01:30:00',NULL,0,NULL,2002),(30007,1012,4008,1,20008,1,1,'2023-04-14','08:30:00',1,0,NULL,NULL),(30008,1008,4008,1,NULL,2,0,'2022-04-26','09:00:00',0,1,'Patient failed to confirm',NULL),(30034,1012,4008,0,NULL,2,0,'2023-04-18','15:30:00',NULL,1,'Canceled by Doctor.',NULL),(30035,1012,4008,1,20006,1,1,'2023-04-07','13:30:00',1,0,NULL,NULL),(30037,1012,4009,1,NULL,3,1,'2023-04-13','05:00:00',1,0,NULL,2001),(30038,1012,4010,1,NULL,3,1,'2023-04-12','10:30:00',1,0,NULL,2001),(30042,1012,4011,0,NULL,1,NULL,'2023-04-19','12:00:00',0,1,'Canceled by Doctor.',NULL),(30043,1012,4003,0,NULL,2,NULL,'2023-04-20','03:30:00',NULL,1,'Canceled by Patient.',NULL),(30044,1012,4003,1,NULL,1,1,'2023-04-18','04:00:00',0,1,'Patient failed to confirm',2006),(30045,1007,4008,0,NULL,2,1,'2023-05-05','05:00:00',NULL,1,'Canceled by Patient.',2006),(30046,1012,4003,0,NULL,1,NULL,'2023-04-14','03:00:00',NULL,1,NULL,NULL),(30049,1012,4003,0,NULL,1,NULL,'2023-04-24','02:00:00',NULL,1,'Canceled by Doctor.',NULL),(30050,1012,4003,0,NULL,1,NULL,'2023-04-26','05:30:00',NULL,1,'Canceled by Patient.',NULL),(30051,1012,4003,0,NULL,2,NULL,'2023-04-25','03:30:00',NULL,1,'Canceled by Patient.',NULL),(30052,1012,4003,0,NULL,1,NULL,'2023-04-25','05:00:00',NULL,1,'Canceled by Patient.',NULL),(30053,1012,4003,0,NULL,1,NULL,'2023-04-24','01:00:00',NULL,1,'Canceled by Doctor.',NULL),(30054,1001,4003,0,NULL,1,0,'2023-04-19','03:30:00',NULL,1,'Referral Denied',NULL),(30055,1001,4003,0,NULL,1,0,'2023-04-26','06:00:00',NULL,1,'Referral Denied',NULL),(30056,1012,4008,1,NULL,2,NULL,'2023-04-26','10:30:00',1,1,'Canceled by Patient.',2006),(30057,1014,4006,0,NULL,1,1,'2023-03-28','11:30:00',NULL,1,'Patient failed to confirm',2001),(30058,1029,4006,1,NULL,2,NULL,'2023-04-18','01:30:00',0,1,'Patient failed to confirm',2006),(30059,1029,4022,0,NULL,2,NULL,'2023-04-19','01:30:00',NULL,1,'Patient failed to confirm',2006),(30060,1003,4008,1,NULL,3,1,'2023-04-25','03:00:00',1,1,'Canceled by Patient.',NULL),(30061,1012,4023,1,20026,1,1,'2023-03-20','03:00:00',1,1,'Canceled by Patient.',2006),(30062,1012,4024,0,NULL,1,NULL,'2023-04-19','01:00:00',NULL,1,'Patient failed to confirm',2006),(30063,1007,4008,1,20028,1,1,'2023-03-28','08:00:00',1,1,NULL,NULL),(30064,1026,4008,0,NULL,2,0,'2023-04-18','08:00:00',NULL,1,'Canceled by Patient.',NULL),(30065,1017,4008,0,NULL,1,1,'2023-04-25','08:30:00',NULL,1,'Canceled by Patient.',2001),(30066,1007,4008,1,20029,1,1,'2023-03-21','10:00:00',1,1,NULL,2001),(30067,1012,4007,1,NULL,1,1,'2023-04-17','01:30:00',1,0,NULL,NULL),(30068,1012,4000,1,NULL,3,1,'2023-04-14','11:30:00',1,0,NULL,NULL),(30069,1007,4008,0,NULL,1,0,'2023-04-21','03:00:00',NULL,1,'Referral Denied',2006),(30070,1012,4008,0,NULL,3,NULL,'2023-04-28','12:00:00',NULL,1,'Canceled by Patient.',NULL),(30071,1012,4018,1,NULL,3,1,'2023-04-11','02:30:00',1,0,NULL,NULL),(30072,1012,4008,1,NULL,3,NULL,'2023-04-21','11:00:00',1,1,'Canceled by Patient.',NULL),(30073,1007,4008,1,NULL,1,1,'2023-04-21','02:00:00',1,1,'Canceled by Patient.',2006),(30074,1012,4008,1,NULL,1,NULL,'2023-04-26','08:00:00',0,0,NULL,2006),(30075,1012,4008,1,NULL,3,NULL,'2023-04-28','10:00:00',1,0,NULL,NULL),(30076,1007,4008,1,NULL,1,1,'2023-04-21','05:00:00',1,1,'Canceled by Patient.',2006),(30077,1005,4008,0,NULL,4,0,'2023-04-28','04:00:00',NULL,1,'Canceled by Patient.',NULL),(30078,1007,4008,1,NULL,1,1,'2023-04-21','04:30:00',1,1,'Canceled by Patient.',2006),(30079,1028,4025,0,NULL,3,NULL,'2023-04-19','01:00:00',NULL,1,'Patient failed to confirm',NULL),(30080,1012,4008,0,NULL,3,NULL,'2023-04-21','10:30:00',NULL,1,'Canceled by Patient.',NULL),(30081,1007,4008,1,NULL,3,1,'2023-04-24','02:30:00',1,0,NULL,NULL),(30082,1012,4008,0,NULL,3,NULL,'2023-04-27','03:00:00',NULL,1,'Canceled by Patient.',NULL),(30083,1012,4008,0,NULL,1,NULL,'2023-04-26','11:30:00',NULL,0,NULL,2001),(30084,1012,4008,0,NULL,1,NULL,'2023-04-26','12:30:00',NULL,0,NULL,2006),(30085,1015,4008,0,NULL,1,1,'2023-05-04','03:30:00',NULL,0,NULL,2006),(30086,1012,4008,1,NULL,1,NULL,'2023-04-25','10:30:00',1,0,NULL,2006);
/*!40000 ALTER TABLE `appointment` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`dbadmin`@`%`*/ /*!50003 TRIGGER `update_appointment_approval` BEFORE INSERT ON `appointment` FOR EACH ROW BEGIN
  DECLARE specialist_check varchar(20);
  SELECT DOCTOR.specialty INTO specialist_check FROM DOCTOR WHERE doctorID = NEW.doctorID;
  
IF (specialist_check NOT LIKE 'PCP') THEN
	SET NEW.referral = 0;
ELSE
	SET NEW.referral = NULL;
END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`dbadmin`@`%`*/ /*!50003 TRIGGER `modify_app` BEFORE UPDATE ON `appointment` FOR EACH ROW BEGIN
  IF NEW.appointmentDate != OLD.appointmentDate OR NEW.appointmentTime != OLD.appointmentTime THEN
    SET NEW.approval = 0, NEW.patient_confirm = NULL;
  END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`dbadmin`@`%`*/ /*!50003 TRIGGER `reset_confirm` BEFORE UPDATE ON `appointment` FOR EACH ROW BEGIN
IF (NEW.PATIENT_CONFIRM = 1 AND OLD.PATIENT_CONFIRM =1 AND OLD.approval = 1 AND NEW.approval = 0) THEN
	SET NEW.PATIENT_CONFIRM = NULL;
END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`dbadmin`@`%`*/ /*!50003 TRIGGER `prevent_nurse_change` BEFORE UPDATE ON `appointment` FOR EACH ROW BEGIN
IF (NEW.nurseID != OLD.nurseID AND (OLD.nurseID IS NOT NULL OR NEW.nurseID IS NOT NULL)) THEN
	SET NEW.nurseID = OLD.nurseID;
END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`dbadmin`@`%`*/ /*!50003 TRIGGER `update_appointment_confirm` BEFORE UPDATE ON `appointment` FOR EACH ROW BEGIN
IF (NEW.Approval = 1 AND OLD.Approval = 0 AND (NEW.Referral = 1 OR NEW.Referral IS NULL)) THEN
	SET NEW.PATIENT_CONFIRM = 0;
END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`dbadmin`@`%`*/ /*!50003 TRIGGER `prevent_confirm_no_referral_or_approval` BEFORE UPDATE ON `appointment` FOR EACH ROW BEGIN
	IF (NEW.PATIENT_CONFIRM = 1 AND (OLD.PATIENT_CONFIRM IS NULL OR OLD.PATIENT_CONFIRM = 0) AND (OLD.approval = 0 OR OLD.Referral = 0)) THEN
		SET NEW.PATIENT_CONFIRM = NULL;
	END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `doctor`
--

DROP TABLE IF EXISTS `doctor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `doctor` (
  `DoctorID` int NOT NULL AUTO_INCREMENT,
  `Fname` varchar(20) NOT NULL,
  `Mname` varchar(20) DEFAULT NULL,
  `Lname` varchar(20) NOT NULL,
  `Specialty` varchar(20) DEFAULT 'PCP',
  `DOB` date DEFAULT NULL,
  `Work_email` varchar(50) DEFAULT NULL,
  `Phone_num` varchar(12) DEFAULT NULL,
  `archive` tinyint(1) NOT NULL DEFAULT '0',
  `Salary` int DEFAULT NULL,
  PRIMARY KEY (`DoctorID`)
) ENGINE=InnoDB AUTO_INCREMENT=1032 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `doctor`
--

LOCK TABLES `doctor` WRITE;
/*!40000 ALTER TABLE `doctor` DISABLE KEYS */;
INSERT INTO `doctor` VALUES (1000,'John','M','Doe','Cardiology','1980-03-15','john.doe@example.com','1234567890',0,80000),(1001,'Jane','K','Smith','Dermatology','1975-11-07','jane.smith@example.com','1234567891',0,92000),(1002,'David','L','Lee','Neurology','1972-09-23','david.lee@example.com','1234567892',0,104000),(1003,'Sarah','N','Chang','Oncology','1985-05-02','sarah.chang@example.com','1234567893',0,116000),(1004,'Michael','S','Johnson','Cardiology','1978-12-18','michael.johnson@example.com','1234567894',0,128000),(1005,'Emily','P','Nguyen','Dermatology','1983-06-29','emily.nguyen@example.com','1234567895',0,140000),(1006,'William','R','Kim','Neurology','1987-02-11','william.kim@example.com','1234567896',0,152000),(1007,'Hannah','L','Lee','Oncology','1979-08-08','hannah.lee@example.com','1234567897',0,164000),(1008,'Daniel','K','Park','Cardiology','1981-04-05','daniel.park@example.com','1234567898',0,176000),(1012,'Uma','K','Thurman','PCP','1970-05-04','umathurma@example.com','2816555576',0,188000),(1013,'Ava','H','Kim','Dermatology','1976-10-19','ava.kim@example.com','1234567899',0,200000),(1014,'Sarah','J','Smith','Cardiology','1985-03-15','sarah.smith@example.com','9876543210',0,80000),(1015,'Emily','R','Lee','Dermatology','1990-06-23','emily.lee@example.com','5554531212',0,92000),(1016,'Michael','J','Jones','Neurology','1980-07-10','michael.jones@example.com','1232267890',0,104000),(1017,'Linda','K','Kim','Neurology','1985-11-02','linda.kim@example.com','9876543210',0,116000),(1018,'Rachel','A','Smith','Nephrology','1983-04-15','rachel.smith@example.com','1234567890',0,128000),(1019,'John','D','Nguyen','Nephrology','1975-11-12','john.nguyen@example.com','2345678901',0,140000),(1020,'Maria','G','Gonzalez','Nephrology','1990-02-28','maria.gonzalez@example.com','3456789012',0,152000),(1021,'David','S','Choi','Nephrology','1988-08-03','david.choi@example.com','4567890123',0,164000),(1022,'Sarah','R','Lee','Internal Medicine','1982-05-10','sarah.lee@example.com','5678901234',0,176000),(1023,'Brian','M','Wong','Internal Medicine','1978-01-22','brian.wong@example.com','6789012345',0,188000),(1024,'Jennifer','E','Davis','Internal Medicine','1991-11-08','jennifer.davis@example.com','7890123456',0,200000),(1025,'Jason','T','Kim','Internal Medicine','1986-03-24','jason.kim@example.com','8901234567',0,80000),(1026,'Lisa','J','Brown','Oncology','1976-09-17','lisa.brown@example.com','9012345678',0,92000),(1027,'Robert','W','Taylor','Oncology','1981-07-02','robert.taylor@example.com','0123456789',0,104000),(1028,'Michelle','L','Johnson','PCP','1984-06-11','michelle.johnson@example.com','1112223333',0,116000),(1029,'Linda','K','Nguyen','PCP','1990-03-08','linda.nguyen@example.com','2223334444',0,128000),(1030,'Julia','M','Garcia','PCP','1985-12-20','julia.garcia@example.com','9876543210',0,140000);
/*!40000 ALTER TABLE `doctor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `emergency_contact_employee`
--

DROP TABLE IF EXISTS `emergency_contact_employee`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `emergency_contact_employee` (
  `fName` varchar(20) DEFAULT NULL,
  `lName` varchar(20) DEFAULT NULL,
  `relation` varchar(20) DEFAULT NULL,
  `Phone_num` char(10) DEFAULT NULL,
  `email` varchar(50) DEFAULT NULL,
  `relationID` int DEFAULT NULL,
  `staffID` int NOT NULL,
  KEY `EMERGENCY_CONTACT_EMPLOYEE_ibfk_1` (`staffID`),
  CONSTRAINT `EMERGENCY_CONTACT_EMPLOYEE_ibfk_1` FOREIGN KEY (`staffID`) REFERENCES `staff` (`StaffID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `emergency_contact_employee`
--

LOCK TABLES `emergency_contact_employee` WRITE;
/*!40000 ALTER TABLE `emergency_contact_employee` DISABLE KEYS */;
/*!40000 ALTER TABLE `emergency_contact_employee` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `emergency_contact_patient`
--

DROP TABLE IF EXISTS `emergency_contact_patient`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `emergency_contact_patient` (
  `fullname` varchar(50) DEFAULT NULL,
  `Relation` varchar(20) DEFAULT NULL,
  `Phone_num` char(10) DEFAULT NULL,
  `email` varchar(50) DEFAULT NULL,
  `patientID` int NOT NULL,
  UNIQUE KEY `patientID` (`patientID`),
  UNIQUE KEY `email` (`email`),
  KEY `EMERGENCY_CONTACT_PATIENT_ibfk_1` (`patientID`),
  CONSTRAINT `EMERGENCY_CONTACT_PATIENT_ibfk_1` FOREIGN KEY (`patientID`) REFERENCES `patients` (`PatientID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `emergency_contact_patient`
--

LOCK TABLES `emergency_contact_patient` WRITE;
/*!40000 ALTER TABLE `emergency_contact_patient` DISABLE KEYS */;
INSERT INTO `emergency_contact_patient` VALUES ('John Smith','Father','1234567890','example1@gmail.com',4000),('Jane Doe','Mother','9876543210','example2@gmail.com',4001),('Mike Johnson','Brother','5555555555','example3@gmail.com',4002),('Lisa Thompson','Sister','1111111111','example4@gmail.com',4003),('David Lee','Friend','2222222222','example5@gmail.com',4004),('Emily Davis','Cousin','3333333333','example6@gmail.com',4005),('Robert Baker','Uncle','4444444444','example7@gmail.com',4006),('Karen Lewis','Aunt','6666666666','example8@gmail.com',4007),('Jessica','brother','4323413421','example23@gmail.com',4008),('John cena','Brother','9231312312','sam@yahoo.com',4009),('Mark Thompson','Cousin','9999999999','example10@gmail.com',4010),('Jessica','Sister','9876543210','example@gmail.com',4011),('Steven Wright','Brother','4443332222','example11@gmail.com',4012),('Laura White','Sister','5556667777','example12@gmail.com',4013),('Nathan Brown','Father','1234567890','example13@gmail.com',4014),('Julia Green','Mother','9876543210','example14@gmail.com',4015),('Alex Hernandez','Brother','5555555555','example15@gmail.com',4016),('Maria Rodriguez','Sister','1111111111','example16@gmail.com',4017),('Chris Davis','Friend','2222222222','example17@gmail.com',4018),('Amanda Smith','Cousin','3333333333','example18@gmail.com',4019),('Patrick Wilson','Uncle','4444444444','example19@gmail.com',4020),('Adam Sandler','Homie','2202003200','sandle@yahoo.com',4024),('jessica','sister','1239399292','jess@gmail.com',4025);
/*!40000 ALTER TABLE `emergency_contact_patient` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `evaluation`
--

DROP TABLE IF EXISTS `evaluation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `evaluation` (
  `code` int NOT NULL AUTO_INCREMENT,
  `test` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`code`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `evaluation`
--

LOCK TABLES `evaluation` WRITE;
/*!40000 ALTER TABLE `evaluation` DISABLE KEYS */;
INSERT INTO `evaluation` VALUES (1,'Blood Work'),(2,'X-ray'),(3,'Ultrasound'),(4,'Lumbar Puncture'),(5,'PET/CAT Scan'),(6,'MRI'),(7,'Vaccination'),(8,'Biopsy');
/*!40000 ALTER TABLE `evaluation` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `insurance`
--

DROP TABLE IF EXISTS `insurance`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `insurance` (
  `InsuranceID` int NOT NULL AUTO_INCREMENT,
  `group_no` varchar(50) DEFAULT NULL,
  `Insurance_Name` varchar(50) DEFAULT NULL,
  `patientID` int DEFAULT NULL,
  PRIMARY KEY (`InsuranceID`),
  KEY `patientID` (`patientID`),
  CONSTRAINT `insurance_ibfk_1` FOREIGN KEY (`patientID`) REFERENCES `patients` (`PatientID`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `insurance`
--

LOCK TABLES `insurance` WRITE;
/*!40000 ALTER TABLE `insurance` DISABLE KEYS */;
INSERT INTO `insurance` VALUES (8,'W321320','Aetna',4008),(9,'W342433','CVS Health',4012),(10,'W342336','Bright Healthcare',4019);
/*!40000 ALTER TABLE `insurance` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`dbadmin`@`%`*/ /*!50003 TRIGGER `connect_insurance` AFTER INSERT ON `insurance` FOR EACH ROW UPDATE patients
    SET insuranceID = NEW.insuranceID
    WHERE patientID = NEW.patientID */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `invoice`
--

DROP TABLE IF EXISTS `invoice`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `invoice` (
  `invoiceID` int NOT NULL AUTO_INCREMENT,
  `total` decimal(10,2) DEFAULT NULL,
  `claim` decimal(10,2) DEFAULT NULL,
  `paid_amount` decimal(10,2) DEFAULT NULL,
  `patientID` int DEFAULT NULL,
  `reportID` int DEFAULT NULL,
  `due_date` date DEFAULT NULL,
  PRIMARY KEY (`invoiceID`),
  KEY `INVOICE_ibfk_1` (`patientID`),
  KEY `reportID_fk` (`reportID`),
  CONSTRAINT `INVOICE_ibfk_1` FOREIGN KEY (`patientID`) REFERENCES `patients` (`PatientID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `reportID_fk` FOREIGN KEY (`reportID`) REFERENCES `visit_details` (`reportID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=10031 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `invoice`
--

LOCK TABLES `invoice` WRITE;
/*!40000 ALTER TABLE `invoice` DISABLE KEYS */;
INSERT INTO `invoice` VALUES (10007,2321.00,2000.00,60.00,4008,20006,'2023-05-07'),(10008,1231.30,23.42,60.00,4008,20008,'2023-05-08'),(10025,130.00,100.00,30.00,4004,20025,'2023-05-17'),(10026,12.00,12.00,0.00,4023,20026,'2023-05-18'),(10027,150.00,120.00,30.00,4001,20027,'2023-05-18'),(10028,100.00,80.00,20.00,4008,20028,'2023-05-18'),(10029,150.00,110.00,40.00,4008,20029,'2023-05-18');
/*!40000 ALTER TABLE `invoice` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`dbadmin`@`%`*/ /*!50003 TRIGGER `add_invoice_due_date` BEFORE INSERT ON `invoice` FOR EACH ROW SET NEW.due_date = DATE_ADD(CURRENT_DATE(), INTERVAL 30 DAY) */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`dbadmin`@`%`*/ /*!50003 TRIGGER `update_patID` BEFORE INSERT ON `invoice` FOR EACH ROW BEGIN
    DECLARE appt_patientID INT;
    SELECT patientID INTO appt_patientID FROM appointment WHERE appointment.reportID = NEW.reportID;
    SET NEW.patientID = appt_patientID;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`dbadmin`@`%`*/ /*!50003 TRIGGER `update_patient_total` AFTER INSERT ON `invoice` FOR EACH ROW UPDATE PATIENTS
	SET balance = balance + NEW.total - NEW.claim - NEW.paid_amount
	WHERE PATIENTS.patientID = NEW.patientID */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`dbadmin`@`%`*/ /*!50003 TRIGGER `update_invoiceID` AFTER INSERT ON `invoice` FOR EACH ROW UPDATE visit_details
    SET visit_details.billingID = NEW.invoiceID
    WHERE visit_details.reportID = NEW.reportID */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `login`
--

DROP TABLE IF EXISTS `login`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `login` (
  `username` varchar(40) NOT NULL,
  `passwrd` varchar(40) NOT NULL,
  `email` varchar(40) DEFAULT NULL,
  `patientID` int DEFAULT NULL,
  `doctorID` int DEFAULT NULL,
  `nurseID` int DEFAULT NULL,
  `adminID` int DEFAULT NULL,
  PRIMARY KEY (`username`),
  KEY `patientID` (`patientID`),
  KEY `doctorID` (`doctorID`),
  KEY `nurseID` (`nurseID`),
  CONSTRAINT `login_ibfk_1` FOREIGN KEY (`patientID`) REFERENCES `patients` (`PatientID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `login_ibfk_2` FOREIGN KEY (`doctorID`) REFERENCES `doctor` (`DoctorID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `login_ibfk_3` FOREIGN KEY (`nurseID`) REFERENCES `nurse` (`NID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `login`
--

LOCK TABLES `login` WRITE;
/*!40000 ALTER TABLE `login` DISABLE KEYS */;
INSERT INTO `login` VALUES ('adams','123',NULL,4025,NULL,NULL,NULL),('adamwilliams','123',NULL,NULL,NULL,2001,NULL),('admin123','123',NULL,NULL,NULL,NULL,1),('bob','123',NULL,4003,NULL,NULL,NULL),('bro','123',NULL,4024,NULL,NULL,NULL),('davidlee','123',NULL,NULL,1002,NULL,NULL),('emdavis','123',NULL,4002,NULL,NULL,NULL),('hannahlee','123',NULL,NULL,1007,NULL,NULL),('jagarcia','123',NULL,4007,NULL,NULL,NULL),('janesmith','123',NULL,NULL,1001,NULL,NULL),('jasmith','123',NULL,4013,NULL,NULL,NULL),('jejackson','123',NULL,4017,NULL,NULL,NULL),('jodoe','123',NULL,4012,NULL,NULL,NULL),('jojones','123',NULL,4001,NULL,NULL,NULL),('josmith','123',NULL,4000,NULL,NULL,NULL),('kw','123',NULL,4008,NULL,NULL,NULL),('maharris','123',NULL,4018,NULL,NULL,NULL),('milopez','123',NULL,4009,NULL,NULL,NULL),('mitaylor','123',NULL,4016,NULL,NULL,NULL),('olclark','123',NULL,4006,NULL,NULL,NULL),('sadavis','123',NULL,4015,NULL,NULL,NULL),('sagarcia','123',NULL,4020,NULL,NULL,NULL),('sarahchang','123',NULL,NULL,1003,NULL,NULL),('sarahsmith','123',NULL,NULL,1014,NULL,NULL),('sasmith','123',NULL,4010,NULL,NULL,NULL),('sawilson','123',NULL,4004,NULL,NULL,NULL),('soph','123',NULL,NULL,NULL,2006,NULL),('stparker','123',NULL,4019,NULL,NULL,NULL),('thlee','123',NULL,4005,NULL,NULL,NULL),('uma123','123',NULL,NULL,1012,NULL,NULL),('wijones','123',NULL,4011,NULL,NULL,NULL),('wijones2','123',NULL,4014,NULL,NULL,NULL);
/*!40000 ALTER TABLE `login` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `nurse`
--

DROP TABLE IF EXISTS `nurse`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `nurse` (
  `NID` int NOT NULL AUTO_INCREMENT,
  `Fname` varchar(20) NOT NULL,
  `Mname` varchar(20) DEFAULT NULL,
  `Lname` varchar(20) NOT NULL,
  `Work_email` varchar(50) DEFAULT NULL,
  `pay` decimal(10,2) DEFAULT NULL,
  `officeID` int DEFAULT NULL,
  `Phone_num` varchar(20) DEFAULT NULL,
  `DOB` date DEFAULT NULL,
  `archive` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`NID`),
  KEY `NURSE_ibfk_1` (`officeID`),
  CONSTRAINT `NURSE_ibfk_1` FOREIGN KEY (`officeID`) REFERENCES `office` (`officeID`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2045 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `nurse`
--

LOCK TABLES `nurse` WRITE;
/*!40000 ALTER TABLE `nurse` DISABLE KEYS */;
INSERT INTO `nurse` VALUES (2000,'Ella','J.','Martinez','ella.martinez@example.com',50000.00,2,'+11234567890','1990-01-01',0),(2001,'Adam','L.','Williams','adam.williams@example.com',55000.00,1,'+12345678901','1985-12-31',0),(2002,'Haley','P.','Rodriguez','haley.rodriguez@example.com',52000.00,2,'+13456789012','1995-06-01',0),(2003,'Isaac','S.','Garcia','isaac.garcia@example.com',48000.00,1,'+14567890123','1980-12-01',0),(2004,'Lily','K.','Johnson','lily.johnson@example.com',53000.00,1,'+15678901234','1987-09-01',0),(2005,'Oscar','R.','Lee','oscar.lee@example.com',51000.00,1,'+16789012345','1992-04-01',0),(2006,'Sophia','H.','Gonzalez','sophia.gonzalez@example.com',49000.00,1,'+17890123456','1977-11-01',0),(2007,'Miles','K.','Davis','miles.davis@example.com',54000.00,1,'+18901234567','1983-08-01',0),(2008,'Zoe','N.','Jackson','zoe.jackson@example.com',57000.00,1,'+19012345678','1972-02-01',0),(2009,'Felix','M.','Garcia','felix.garcia@example.com',56000.00,1,'+10987654321','1989-07-01',0),(2041,'Gerald','G','Williams','geraldwiliams@uma.com',18000.00,3,'2314555557','1985-06-13',0),(2043,'Sam','r','Smithing','sam@gmail.com',NULL,3,'1111111111','1985-12-18',0),(2044,'Dennis','S','Steve','example@gmail.com',NULL,3,'1111111111','2023-04-11',0);
/*!40000 ALTER TABLE `nurse` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`dbadmin`@`%`*/ /*!50003 TRIGGER `calculate_total_nurses` AFTER INSERT ON `nurse` FOR EACH ROW UPDATE office
	SET numNurses = numNurses + 1
    WHERE NEW.officeID = officeID */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `office`
--

DROP TABLE IF EXISTS `office`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `office` (
  `officeID` int NOT NULL,
  `officeAddress` varchar(80) DEFAULT NULL,
  `phone_num` varchar(15) DEFAULT NULL,
  `email` varchar(50) DEFAULT NULL,
  `archive` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`officeID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `office`
--

LOCK TABLES `office` WRITE;
/*!40000 ALTER TABLE `office` DISABLE KEYS */;
INSERT INTO `office` VALUES (1,'123 Main St., Houston, TX, 77070','281-965-9439','houstonbilling@example.com',0),(2,'134 Baker St., Miami, FL, 34532','786-513-9432','miamibilling@example.com',0),(3,'567 Pine Street, Houston, TX 77002','713-357-8898','houstonbilling@example.com',0),(4,'1234 Elm Avenue, Miami, FL 33129','786-221-8207','miamibilling@example.com',0);
/*!40000 ALTER TABLE `office` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `patients`
--

DROP TABLE IF EXISTS `patients`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `patients` (
  `PatientID` int NOT NULL AUTO_INCREMENT,
  `fName` varchar(20) DEFAULT NULL,
  `lName` varchar(20) DEFAULT NULL,
  `Minitial` varchar(10) DEFAULT NULL,
  `DOB` date DEFAULT NULL,
  `Address` varchar(50) DEFAULT NULL,
  `Phone_num` char(10) DEFAULT NULL,
  `email` varchar(50) DEFAULT NULL,
  `balance` int DEFAULT '0',
  `insuranceID` int DEFAULT NULL,
  `doctorID` int DEFAULT NULL,
  `payment` int DEFAULT '0',
  `gender` varchar(20) DEFAULT 'other',
  `pharmacy_name` varchar(50) DEFAULT NULL,
  `pharmacy_address` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`PatientID`),
  KEY `PATIENTS_ibfk_2` (`insuranceID`),
  KEY `PATIENTS_ibfk_3` (`doctorID`),
  CONSTRAINT `patients_ibfk_1` FOREIGN KEY (`insuranceID`) REFERENCES `insurance` (`InsuranceID`),
  CONSTRAINT `PATIENTS_ibfk_3` FOREIGN KEY (`doctorID`) REFERENCES `doctor` (`DoctorID`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3000 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patients`
--

LOCK TABLES `patients` WRITE;
/*!40000 ALTER TABLE `patients` DISABLE KEYS */;
INSERT INTO `patients` VALUES (4000,'John','Smith','P','1980-05-15','123 Main St, Houston, TX, 77002','5551234567','medicalclinicdb@gmail.com',0,NULL,1012,0,'other','CVS Health','123 Greenbriar Dr, Houston, TX, 77380'),(4001,'John','Smith','D','1990-01-15','123 Main St, Houston, TX, 77001','5551234567','medicalclinicdb@gmail.com',0,NULL,1028,0,'other','CVS Health','123 Greenbriar Dr, Houston, TX, 77380'),(4002,'Emily','Davis','A','1985-05-10','789 Elm St, Miami, FL, 33102','5552345678','medicalclinicdb@gmail.com',0,NULL,1029,0,'other','CVS Health','123 Greenbriar Dr, Houston, TX, 77380'),(4003,'Bo','Bichette','R','1972-12-07','123 Main St, Houston, TX, 77001','5553456789','medicalclinicdb@gmail.com',0,NULL,1012,0,'other','CVS Health','123 Greenbriar Dr, Houston, TX, 77380'),(4004,'Sarah','Wilson','L','1993-09-20','789 Elm St, Miami, FL, 33102','5554567890','medicalclinicdb@gmail.com',0,NULL,1030,0,'other','CVS Health','123 Greenbriar Dr, Houston, TX, 77380'),(4005,'Thomas','Lee','S','1980-03-18','123 Main St, Houston, TX, 77001','5555678901','medicalclinicdb@gmail.com',0,NULL,1028,0,'other','CVS Health','123 Greenbriar Dr, Houston, TX, 77380'),(4006,'Olivia','Clark','E','1998-06-25','789 Elm St, Miami, FL, 33102','5556789012','medicalclinicdb@gmail.com',0,NULL,1029,0,'other','CVS Health','123 Greenbriar Dr, Houston, TX, 77380'),(4007,'James','Garcia','R','1975-02-14','123 Main St, Houston, TX, 77001','5557890123','medicalclinicdb@gmail.com',0,NULL,1012,0,'other','CVS Health','123 Greenbriar Dr, Houston, TX, 77380'),(4008,'Kayla','Weigle','R','1991-12-30','789 Elm St, Miami, FL, 33102','5558901234','medicalclinicdb@gmail.com',24129,8,1012,1361,'other','CVS Health','123 Greenbriar Dr, Houston, TX, 77380'),(4009,'Michael','Lopez','T','1988-08-05','123 Main St, Houston, TX, 77001','5559012345','medicalclinicdb@gmail.com',0,NULL,1028,0,'other','CVS Health','123 Greenbriar Dr, Houston, TX, 77380'),(4010,'Samantha','Smith','L','1985-05-12','789 Broadway Ave, Miami, FL, 33102','5551234567','medicalclinicdb@gmail.com',0,NULL,1029,0,'other','CVS Health','123 Greenbriar Dr, Houston, TX, 77380'),(4011,'William','Jones','R','1970-12-25','456 Oak Ave, Miami, FL, 33101','5551112222','medicalclinicdb@gmail.com',0,NULL,1030,0,'other','CVS Health','123 Greenbriar Dr, Houston, TX, 77380'),(4012,'John','Doe','D','1990-01-01','123 Main St, Houston, TX, 77001','5555555555','medicalclinicdb@gmail.com',0,9,1012,0,'other','CVS Health','123 Greenbriar Dr, Houston, TX, 77380'),(4013,'Jane','Smith','L','1985-05-12','789 Broadway Ave, Miami, FL, 33102','5556667777','medicalclinicdb@gmail.com',0,NULL,1029,0,'other','CVS Health','123 Greenbriar Dr, Houston, TX, 77380'),(4014,'William','Jones','R','1970-12-25','456 Oak Ave, Miami, FL, 33101','5558889999','medicalclinicdb@gmail.com',0,NULL,1030,0,'other','CVS Health','123 Greenbriar Dr, Houston, TX, 77380'),(4015,'Samantha','Davis','A','1998-08-10','123 Main St, Houston, TX, 77001','5557778888','medicalclinicdb@gmail.com',0,NULL,1028,0,'other','CVS Health','426 Sunshine St, Ft. Lauderdale, FL, 53438'),(4016,'Michael','Taylor','T','1977-02-28','789 Broadway Ave, Miami, FL, 33102','5554443333','medicalclinicdb@gmail.com',0,NULL,1029,0,'other','CVS Health','426 Sunshine St, Ft. Lauderdale, FL, 53438'),(4017,'Jennifer','Jackson','J','1992-11-15','456 Oak Ave, Miami, FL, 33101','5553332222','medicalclinicdb@gmail.com',0,NULL,1030,0,'other','CVS Health','426 Sunshine St, Ft. Lauderdale, FL, 53438'),(4018,'Matthew','Harris','H','1980-07-03','123 Main St, Houston, TX, 77001','5552221111','medicalclinicdb@gmail.com',0,NULL,1012,0,'other','CVS Health','426 Sunshine St, Ft. Lauderdale, FL, 53438'),(4019,'Stephanie','Parker','P','1995-04-18','789 Broadway Ave, Miami, FL, 33102','5559998888','medicalclinicdb@gmail.com',0,10,1029,0,'other','CVS Health','426 Sunshine St, Ft. Lauderdale, FL, 53438'),(4020,'Sarah','Garcia','M','1990-02-15','123 Main St, Houston, TX, 77001','5551234567','medicalclinicdb@gmail.com',0,NULL,1028,0,'other','CVS Health','426 Sunshine St, Ft. Lauderdale, FL, 53438'),(4022,'Daniel','Grant','G','1992-09-24','4354 Regal TX 67789','2124480967','medicalclinicdb@gmail.com',0,NULL,1029,0,'Male','CVS Health','426 Sunshine St, Ft. Lauderdale, FL, 53438'),(4023,'Henry','Clay','','2023-04-03','9222 Memorial','2313123123','medicalclinicdb@gmail.com',0,NULL,1012,0,'other','CVS Health','426 Sunshine St, Ft. Lauderdale, FL, 53438'),(4024,'Georg','Ninka','','2002-09-11','drop the top','1288298839','medicalclinicdb@gmail.com',0,NULL,1012,0,'other','CVS Health','426 Sunshine St, Ft. Lauderdale, FL, 53438'),(4025,'Adam','Sandler','','2019-01-29','3426 Yale Street MGW528','1229339000','medicalclinicdb@gmail.com',0,NULL,1028,0,'other','CVS Health','426 Sunshine St, Ft. Lauderdale, FL, 53438');
/*!40000 ALTER TABLE `patients` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `prescriptions`
--

DROP TABLE IF EXISTS `prescriptions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `prescriptions` (
  `NDC` varchar(50) DEFAULT NULL,
  `drug_name` varchar(50) DEFAULT NULL,
  `dosage` varchar(50) DEFAULT NULL,
  `refills` int DEFAULT NULL,
  `patientID` int NOT NULL,
  `prescriptionID` int NOT NULL AUTO_INCREMENT,
  `notes` varchar(1000) DEFAULT NULL,
  `reportID` int DEFAULT NULL,
  `prescribed_date` date DEFAULT NULL,
  `doctorID` int DEFAULT NULL,
  PRIMARY KEY (`prescriptionID`),
  KEY `patientID` (`patientID`),
  KEY `doctorID` (`doctorID`),
  CONSTRAINT `prescriptions_ibfk_1` FOREIGN KEY (`patientID`) REFERENCES `patients` (`PatientID`),
  CONSTRAINT `prescriptions_ibfk_2` FOREIGN KEY (`doctorID`) REFERENCES `doctor` (`DoctorID`)
) ENGINE=InnoDB AUTO_INCREMENT=5009 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prescriptions`
--

LOCK TABLES `prescriptions` WRITE;
/*!40000 ALTER TABLE `prescriptions` DISABLE KEYS */;
INSERT INTO `prescriptions` VALUES ('8321-3sd-dda','Ibuprofen','20mg',3,4008,5000,'Take on daily with bread.',NULL,'2023-04-14',1012),('303-ds-dsa-32','Doxycycline','30mg',3,4008,5001,'Take twice daily with meal.',20006,'2023-04-14',1012),('42354234','Analgesic','30mg',3,4008,5002,'Take once a day',20008,'2023-04-18',1012),('42354234','Analgesic','30mg',3,4008,5003,'Take once a day',20008,'2023-04-18',1012),('34234-ab-edsf','doxycycline','40mg',3,4009,5004,'Take every day!',NULL,'2023-04-18',1012),('12345-6789-10','Amoxillin','500 mg',2,4007,5005,'Take with food, every 8 hours for 10 days.',NULL,'2023-04-18',1012),('98765-4321-01','Oxycodone','10 mg',0,4007,5006,'Take with food as needed for pain, do not exceed 4 tablets per day.',NULL,'2023-04-18',1012),(' 12345-6789-10','Linsopril','20 mg',2,4000,5007,'Take once daily with or without food, monitor blood pressure regularly.',NULL,'2023-04-18',1012),('12345','Tamiflu','75 mg',0,4001,5008,'once a day',20027,'2023-04-18',1001);
/*!40000 ALTER TABLE `prescriptions` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`dbadmin`@`%`*/ /*!50003 TRIGGER `set_prescription_date` BEFORE INSERT ON `prescriptions` FOR EACH ROW BEGIN
		IF NEW.prescribed_date is NULL THEN
			SET NEW.prescribed_date = current_date();
		END IF;
	END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`dbadmin`@`%`*/ /*!50003 TRIGGER `update_prescriptID` AFTER INSERT ON `prescriptions` FOR EACH ROW UPDATE visit_details
    SET visit_details.prescriptionID = NEW.prescriptionID
    WHERE NEW.reportID = visit_details.reportID */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `schedule`
--

DROP TABLE IF EXISTS `schedule`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `schedule` (
  `Monday` int DEFAULT NULL,
  `Tuesday` int DEFAULT NULL,
  `Wednesday` int DEFAULT NULL,
  `Thursday` int DEFAULT NULL,
  `Friday` int DEFAULT NULL,
  `doctor` int NOT NULL,
  UNIQUE KEY `unique_doctor` (`doctor`),
  KEY `SCHEDULE_ibfk_1` (`doctor`),
  KEY `SCHEDULE_ibfk_2` (`Monday`),
  KEY `SCHEDULE_ibfk_3` (`Wednesday`),
  KEY `SCHEDULE_ibfk_4` (`Thursday`),
  KEY `SCHEDULE_ibfk_5` (`Friday`),
  KEY `SCHEDULE_ibfk_6` (`Tuesday`),
  CONSTRAINT `SCHEDULE_ibfk_1` FOREIGN KEY (`doctor`) REFERENCES `doctor` (`DoctorID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `SCHEDULE_ibfk_2` FOREIGN KEY (`Monday`) REFERENCES `office` (`officeID`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `SCHEDULE_ibfk_3` FOREIGN KEY (`Wednesday`) REFERENCES `office` (`officeID`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `SCHEDULE_ibfk_4` FOREIGN KEY (`Thursday`) REFERENCES `office` (`officeID`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `SCHEDULE_ibfk_5` FOREIGN KEY (`Friday`) REFERENCES `office` (`officeID`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `SCHEDULE_ibfk_6` FOREIGN KEY (`Tuesday`) REFERENCES `office` (`officeID`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `schedule`
--

LOCK TABLES `schedule` WRITE;
/*!40000 ALTER TABLE `schedule` DISABLE KEYS */;
INSERT INTO `schedule` VALUES (1,1,1,3,3,1000),(1,1,3,3,3,1001),(2,2,4,4,4,1002),(1,1,3,3,3,1003),(2,2,4,4,4,1004),(2,2,4,4,4,1005),(4,4,2,2,2,1006),(3,3,1,1,1,1007),(4,4,2,2,2,1008),(1,1,1,3,3,1012),(4,4,2,2,2,1013),(3,3,3,1,1,1014),(3,3,1,1,1,1015),(3,3,3,1,1,1016),(1,1,1,3,3,1017),(4,4,4,2,2,1018),(2,2,2,4,4,1019),(1,1,1,3,3,1020),(3,3,3,1,1,1021),(3,3,1,1,1,1022),(1,1,3,3,3,1023),(2,2,2,4,4,1024),(4,4,4,2,2,1025),(2,2,2,4,4,1026),(4,4,4,2,2,1027),(3,3,3,1,1,1028),(2,2,2,4,4,1029),(4,4,4,2,2,1030);
/*!40000 ALTER TABLE `schedule` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `staff`
--

DROP TABLE IF EXISTS `staff`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `staff` (
  `StaffID` int NOT NULL AUTO_INCREMENT,
  `FName` varchar(20) DEFAULT NULL,
  `LName` varchar(20) DEFAULT NULL,
  `SRole` varchar(20) DEFAULT NULL,
  `Pay` varchar(20) DEFAULT NULL,
  `officeID` int DEFAULT NULL,
  `DOB` date DEFAULT NULL,
  `Work_email` varchar(50) DEFAULT NULL,
  `Phone_num` varchar(12) DEFAULT NULL,
  `archive` tinyint(1) NOT NULL DEFAULT '0',
  `Salary` int DEFAULT NULL,
  PRIMARY KEY (`StaffID`),
  KEY `STAFF_ibfk_2` (`officeID`),
  CONSTRAINT `STAFF_ibfk_1` FOREIGN KEY (`officeID`) REFERENCES `office` (`officeID`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3031 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `staff`
--

LOCK TABLES `staff` WRITE;
/*!40000 ALTER TABLE `staff` DISABLE KEYS */;
INSERT INTO `staff` VALUES (3000,'Emma','Martinez','Receptionist','40000',1,'1990-01-01','emma.martinez@example.com','+11234567890',0,23000),(3001,'Jack','Williams','Janitor','35000',1,'1985-12-31','jack.williams@example.com','+11234567891',0,43000),(3002,'Avery','Rodriguez','IT Support','50000',2,'1995-06-01','avery.rodriguez@example.com','+11234567892',0,50000),(3003,'Max','Garcia','HR Manager','70000',2,'1980-12-01','max.garcia@example.com','+11234567893',0,54000),(3004,'Sophie','Johnson','Accountant','60000',1,'1987-09-01','sophie.johnson@example.com','+11234567894',0,12343),(3005,'Ryan','Lee','Marketing Manager','75000',2,'1992-04-01','ryan.lee@example.com','+11234567895',0,28000),(3006,'Aria','Gonzalez','Human Resources','45000',1,'1977-11-01','aria.gonzalez@example.com','+11234567896',0,35000),(3007,'Oliver','Davis','IT Manager','80000',2,'1983-08-01','oliver.davis@example.com','+11234567897',0,52000),(3008,'Chloe','Jackson','Secretary','55000',1,'1975-02-01','chloe.jackson@example.com','+11234567898',0,43023),(3009,'Leo','Garcia','Executive Assistant','65000',2,'1989-07-01','leo.garcia@example.com','+11234567899',0,23211),(3030,'Polly','Dun','Desk Assistant',NULL,1,'1993-06-17','pollydun@gmail.com','4457894432',0,40000);
/*!40000 ALTER TABLE `staff` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `test`
--

DROP TABLE IF EXISTS `test`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `test` (
  `test_code` int DEFAULT NULL,
  `patientID` int DEFAULT NULL,
  `officeID` int DEFAULT NULL,
  `test_date` date DEFAULT NULL,
  `testID` int NOT NULL AUTO_INCREMENT,
  `nurseID` int DEFAULT NULL,
  `test_time` time DEFAULT NULL,
  PRIMARY KEY (`testID`),
  KEY `patientID` (`patientID`),
  KEY `officeID` (`officeID`),
  KEY `test_code` (`test_code`),
  KEY `nurseID` (`nurseID`),
  CONSTRAINT `test_ibfk_1` FOREIGN KEY (`patientID`) REFERENCES `patients` (`PatientID`),
  CONSTRAINT `test_ibfk_2` FOREIGN KEY (`officeID`) REFERENCES `office` (`officeID`),
  CONSTRAINT `test_ibfk_3` FOREIGN KEY (`test_code`) REFERENCES `evaluation` (`code`),
  CONSTRAINT `test_ibfk_4` FOREIGN KEY (`nurseID`) REFERENCES `nurse` (`NID`)
) ENGINE=InnoDB AUTO_INCREMENT=105 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `test`
--

LOCK TABLES `test` WRITE;
/*!40000 ALTER TABLE `test` DISABLE KEYS */;
INSERT INTO `test` VALUES (2,4008,3,'2023-05-05',100,NULL,'04:00:00'),(3,4008,1,'2023-04-24',101,2006,'02:30:00'),(1,4008,1,'2023-05-03',102,2006,'04:00:00'),(6,4008,1,'2023-04-25',103,2001,'03:00:00'),(7,4008,1,'2023-04-28',104,2006,'02:30:00');
/*!40000 ALTER TABLE `test` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`dbadmin`@`%`*/ /*!50003 TRIGGER `prevent_nurse_change_test` BEFORE UPDATE ON `test` FOR EACH ROW BEGIN
IF (NEW.nurseID != OLD.nurseID AND (OLD.nurseID IS NOT NULL OR NEW.nurseID IS NOT NULL)) THEN
	SET NEW.nurseID = OLD.nurseID;
END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `visit_details`
--

DROP TABLE IF EXISTS `visit_details`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `visit_details` (
  `reportID` int NOT NULL AUTO_INCREMENT,
  `prescription` varchar(100) DEFAULT NULL,
  `Diagnosis` varchar(100) DEFAULT NULL,
  `appointmentID` int NOT NULL,
  `billingID` int DEFAULT NULL,
  `temperature` varchar(3) DEFAULT NULL,
  `symptoms` varchar(200) DEFAULT NULL,
  `nurseID` int DEFAULT NULL,
  `heartRate` int DEFAULT NULL,
  `furtherEval` int DEFAULT NULL,
  `prescriptionID` int DEFAULT NULL,
  `prescriptionRequired` tinyint(1) DEFAULT NULL,
  `height_inches` int DEFAULT NULL,
  `weight_lb` int DEFAULT NULL,
  `bloodPressure_sys` int DEFAULT NULL,
  `bloodPressure_dia` int DEFAULT NULL,
  PRIMARY KEY (`reportID`),
  KEY `REPORT_ibfk_2` (`appointmentID`),
  KEY `REPORT_ibfk_4` (`billingID`),
  KEY `nurseID` (`nurseID`),
  KEY `furtherEval` (`furtherEval`),
  KEY `prescriptionID` (`prescriptionID`),
  CONSTRAINT `REPORT_ibfk_2` FOREIGN KEY (`appointmentID`) REFERENCES `appointment` (`AppointmentID`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `REPORT_ibfk_4` FOREIGN KEY (`billingID`) REFERENCES `invoice` (`invoiceID`) ON UPDATE CASCADE,
  CONSTRAINT `visit_details_ibfk_1` FOREIGN KEY (`nurseID`) REFERENCES `nurse` (`NID`),
  CONSTRAINT `visit_details_ibfk_2` FOREIGN KEY (`furtherEval`) REFERENCES `evaluation` (`code`),
  CONSTRAINT `visit_details_ibfk_3` FOREIGN KEY (`prescriptionID`) REFERENCES `prescriptions` (`prescriptionID`)
) ENGINE=InnoDB AUTO_INCREMENT=20031 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `visit_details`
--

LOCK TABLES `visit_details` WRITE;
/*!40000 ALTER TABLE `visit_details` DISABLE KEYS */;
INSERT INTO `visit_details` VALUES (20006,'Anti-vomiting','Pregnant',30035,10007,'97','Big belly',2006,67,3,5001,1,63,130,122,78),(20008,'Analgesic','Broken hand',30007,10008,'96','Pain, swelling in hand',2006,65,2,5003,1,65,131,123,79),(20025,NULL,'Skin Cancer',30005,10025,'96','itchy skin, red spots',2006,67,2,NULL,NULL,68,130,120,80),(20026,'anti suds','suds',30061,10026,'12','cough',2006,12,1,NULL,1,63,140,120,65),(20027,'Tamiflu','Influenza',30001,10027,'101','Fever, cough, headache',2001,68,7,5008,1,65,125,120,80),(20028,'Ibuprofen','Migraine',30063,10028,'97','headache, nausea',2001,66,1,NULL,1,64,132,121,80),(20029,'Celebrex','Osteoarthritis',30066,10029,'98','Joint pain, stiffness',2001,65,1,NULL,1,65,131,122,79);
/*!40000 ALTER TABLE `visit_details` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`dbadmin`@`%`*/ /*!50003 TRIGGER `update_reportID` AFTER INSERT ON `visit_details` FOR EACH ROW UPDATE appointment
    SET appointment.reportID = NEW.reportID
    WHERE appointment.appointmentID = NEW.appointmentID */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`dbadmin`@`%`*/ /*!50003 TRIGGER `update_test` AFTER INSERT ON `visit_details` FOR EACH ROW BEGIN
DECLARE patID int;
SELECT patientID into patID FROM appointment WHERE appointmentID = NEW.appointmentID;
    IF (NEW.furtherEval IS NOT NULL) THEN
		INSERT into test(test_code,patientID)
        VALUES (NEW.furtherEval, patID);
	END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Dumping events for database 'medicalclinicdb2'
--

--
-- Dumping routines for database 'medicalclinicdb2'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-04-23 20:25:12
