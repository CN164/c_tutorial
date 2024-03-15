#!/bin/bash
echo "Start"
cd ./Liquibase/workspace

dbUrl="jdbc:postgresql://postgres:5432/postgres"
dbUserName="postgres"
dbPassword=""

if [ $ASPNETCORE_ENVIRONMENT = "LocalRegression" ] ;\
then\
	dbUrl="jdbc:postgresql://pgsql_ctutorial_regression/postgres"
	dbUserName="postgres"
fi;

cd /app/Liquibase/workspace && \

java -jar liquibase.jar \
	--driver=org.postgresql.Driver \
	--classpath=./lib/postgresql.jar \
	--url=$dbUrl \
	--changeLogFile=./changelog/changelog-initschema.xml \
	--username=$dbUserName\
	--password=$dbPassword\
	--defaultSchemaName=public\
	--databaseChangeLogLockTableName="${SERVICE_NAME}_databasechangeloglock"\
	--databaseChangeLogTableName="${SERVICE_NAME}_databasechangelog"\
	update;
java -jar liquibase.jar \
	--driver=org.postgresql.Driver \
	--classpath=./lib/postgresql.jar \
	--url=$dbUrl \
	--changeLogFile=./changelog/changelog-master.xml \
	--username=$dbUserName\
	--password=$dbPassword\
	--defaultSchemaName=go_tutorial\
	update && cd /app/out \
	&& ./${SERVICE_NAME}
