rm -rf official
git clone --depth=1 git@github.com:kubernetes/website.git
mkdir official && mv website/content/en/examples/* official/
rm -rf website
rm -rf official/examples.go
rm -rf official/examples_test.go
rm -rf official/README.md
