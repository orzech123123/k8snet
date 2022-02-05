0. git clone to /home

1. chmod +x /home/k8snet/api/execpipe.sh

2. crontab -e
	>> dodaj linijke "@reboot /home/k8snet/api/execpipe.sh
	
3. reboot 

4. docker-compose up -d