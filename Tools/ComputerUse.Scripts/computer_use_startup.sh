#!/bin/bash
echo "starting Computer Use"

# Start noVNC with explicit websocket settings
dotnet $COMPUTER_USE_AGENT

echo "Computer Use started successfully"
