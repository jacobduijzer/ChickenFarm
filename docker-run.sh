docker-compose  -f docker-compose.tools.yml \
                -f docker-compose.frontend.yml \
                -f docker-compose.postgresql.yml \
                -f docker-compose.taskservice.yml $1