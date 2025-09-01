#!/bin/bash
echo "replacing firefox-esr configurations"

sudo cp -f $HOME/.config/firefox-esr/syspref.js /lib/firefox-esr/browser/defaults/preferences/syspref.js

echo "firefox-esr configurations were replaced"