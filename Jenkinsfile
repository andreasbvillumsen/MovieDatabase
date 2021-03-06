pipeline {
    agent any
	triggers {
		// cron 'H * * * *'
		pollSCM 'H/3 * * * *'
	}
    stages {
        stage('Docker down') {
            steps {
				sh "docker-compose -p staging down"
                sh "docker-compose -p production down"
			}
		}

		stage('Build web') {
            steps {
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
				sh "docker-compose -p staging up flyway"
            }
        }

        stage("Release staging environment") {
            steps {
				sh "docker-compose -p staging -f docker-compose.yml -f docker-compose.staging.yml up -d web"
            }
        }
    }
}