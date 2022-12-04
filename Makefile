
build:
	dotnet build

runAPI: build
	x-terminal-emulator --hold -e 'dotnet run --no-build --project ./src/DucksNet.API/' &
runUI: build
	x-terminal-emulator --hold -e 'dotnet run --no-build --project ./src/DucksNet.API/' & \
    x-terminal-emulator --hold -e 'dotnet run --no-build --project ./src/DucksNet.WebUI/' &

INTEGRATION_TESTTS=AppointmentsController CagesController EmployeesController MedicalRecordsController

test_integration: build
    # loop to each integration test
	for test in $(INTEGRATION_TESTTS); do \
        # run the test \
        dotnet test --no-build -p:AltCover=true --filter "FullyQualifiedName~DucksNet.IntegrationTests.$$test"; \
        # rename the test file \
        mv tests/DucksNet.IntegrationTests/coverage.xml tests/DucksNet.IntegrationTests/$$test.coverage.xml; \
    done

test_unit: build
	dotnet test --no-build -p:AltCover=true --filter "FullyQualifiedName~DucksNet.UnitTests"
	mv tests/DucksNet.UnitTests/coverage.xml tests/DucksNet.UnitTests/UnitTests.coverage.xml || mv tests/DucksNet.IntegrationTests/coverage.xml tests/DucksNet.UnitTests/UnitTests.coverage.xml

test: test_unit test_integration