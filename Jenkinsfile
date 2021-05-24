pipeline {
    agent any
	triggers {
		// cron 'H * * * *'
		pollSCM 'H/3 * * * *'
	}
    stages {
		stage('Build web') {
            steps {
				// sh "dotnet build MovieDatabase/MovieDatabase.csproj"
				sh "dotnet build"
			}
		}

        stage("Build database") {
            steps {
                echo "===== OPTIONAL: Will build the database (if using a state-based approach) ====="
            }
        }

        stage("Test Web") {
            steps {
                sh "dotnet test XUnitMoviesTest/XUnitMoviesTest.csproj"
            }
        }

		stage("Login on dockerhub") {
			steps {
				withCredentials([[$class: 'UsernamePasswordMultiBinding', credentialsId: 'dockerhub', usernameVariable: 'USERNAME', passwordVariable: 'PASSWORD']])
				{
					sh 'docker login -u ${USERNAME} -p ${PASSWORD}'	
				}
			}
		}

        stage("Deliver Web") {
            steps {
                sh "docker build . -t gruppe1devops/moviedatabase"
				sh "docker push gruppe1devops/moviedatabase"
            }
        }

        stage("Release staging environment") {
            steps {
				sh "docker-compose pull"
				sh "docker-compose up flyway"
				sh "docker-compose -f docker-compose.yml -f docker-compose.prod.yml up -d web"
            }
        }

        stage("Automated acceptance test") {
            steps {
                echo "===== REQUIRED: Will use Selenium to execute automatic acceptance tests ====="
            }
        }
    }
}