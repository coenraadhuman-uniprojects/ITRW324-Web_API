INSERT INTO WeatherDB.ENDUSERS (USERID, FIRSTNAME, LASTNAME, EMAIL, USERNAME, PASSWORD) VALUES ('1','John', 'Doe','28634748@student.g.nwu.ac.za','Morne111','fsdbfhbsdjfbdshhsdfdsfhsdbfsdhfbj');
INSERT INTO WeatherDB.LOCATIONS (LOCATIONID, LOCATIONNAME) VALUES ('1', 'TestLocation');
INSERT INTO WeatherDB.STATIONS (STATIONID,USERID, LOCATIONID, NICKNAME) VALUES ('1344534563','1','1','Johnyboy');
INSERT INTO WeatherDB.READINGS (READINGID, STATIONID, TEMPERATURE, AIRPRESSURE, AMBIENTLIGHT, HUMIDITY) VALUES ('1','1','32.2','250.04','0.34','0.5');
