#!/bin/sh
set -ex

readonly ROOT_DIR=`pwd`
readonly TAG=${TAG:=$(git rev-parse --short HEAD)}
readonly NAMESPACE=${NAMESPACE:="vndg"}
readonly SERVICE_PATH=${ROOT_DIR}/src/services/rating
readonly SERVICE_NAME=cs-rating-service

echo "Namespace is ${NAMESPACE} and tag is ${TAG}"
echo "Start to build ${SERVICE_NAME}..."

docker build -f $SERVICE_PATH/Dockerfile \
    -t $NAMESPACE/$SERVICE_NAME:$TAG \
    -t $NAMESPACE/$SERVICE_NAME:latest .
