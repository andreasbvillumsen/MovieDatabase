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
                sh "docker build . -t gruppe1devops/moviedatabase"
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
				sh "docker push gruppe1devops/moviedatabase"
            }
        }

        stage("Build database") {
            steps {
                sh "docker-compose pull"
				sh "docker-compose up flyway"
            }
        }

        stage("Release staging environment") {
            steps {
				sh "docker-compose  -f docker-compose.yml -f docker-compose.prod.yml up -d web"
            }
        }

        stage("Heroku login") {
            steps {
                sh "docker login --username=andreas@villumsen.org --password=6a1078b8-ea8d-4468-afa8-299ee3c2bd5b registry.heroku.com"
                sh "HEROKU_API_KEY=6a1078b8-ea8d-4468-afa8-299ee3c2bd5b"
            }
        }

        stage("Release to prod") {
            steps {
                sh "heroku container:push dyno --app fierce-wave-66466"
            }
        }
    }
}