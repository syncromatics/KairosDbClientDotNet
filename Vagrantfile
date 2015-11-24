# -*- mode: ruby -*-
# vi: set ft=ruby :

unless Vagrant.has_plugin?("vagrant-docker-compose")
  system("vagrant plugin install vagrant-docker-compose")
  puts "Dependencies installed, please try the command again."
  exit
end

Vagrant.configure("2") do |config|
  config.vm.box = "puphpet/ubuntu1404-x64"

  config.vm.network "forwarded_port", guest: 8888, host: 8888
  config.vm.network "forwarded_port", guest: 8083, host: 8083
  config.vm.network "forwarded_port", guest: 4242, host: 4242
  config.vm.network "forwarded_port", guest: 3000, host: 3000
  config.vm.network "forwarded_port", guest: 3001, host: 3001

  config.vm.provision :docker
  config.vm.provision :docker_compose, yml: "/vagrant/docker-compose.yml", rebuild: true, run: "always", project_name: "chrono"

  config.vm.provision "shell", inline: "curl -X POST http://127.0.0.1:8888/cluster-configs -d '{ \"cassandra\": { \"seed_hosts\": \"dev-cass-1\" }, \"cassandra_metrics\": {}, \"jmx\": { \"port\": \"7199\" } }'"
end